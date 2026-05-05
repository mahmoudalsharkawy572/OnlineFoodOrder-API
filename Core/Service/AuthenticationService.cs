using DomainLayer.Models.Identity;
using DomainLayer.Exceptions;
using Microsoft.AspNetCore.Identity;
using ServicesAbstraction;
using Shared.DataTransferObjects.IdentityDTos;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Shared;
using Shared.OrderModels;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Services
{
    public class AuthenticationService(UserManager<User> _userManager 
                                     , RoleManager<IdentityRole> _roleManager
                                     , IOptions<JwtOptions> options
                                     , IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailExist(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }
        public async Task<AddressDTO> GetUserAddress(string email)
        {
            var address = await _userManager.Users.Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == email) 
                 ?? throw new UserNotFoundException(email);
            
            return _mapper.Map<AddressDTO>(address.Address);
        }
        public async Task<UserResultDTo> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                throw new UserNotFoundException(email);
            return new UserResultDTo(user.DisplayName, email, await CreateTokenAsync(user));
        }
        public async Task<AddressDTO> UpdateUserAddress(AddressDTO address, string email)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == email)
                 ?? throw new UserNotFoundException(email);

            if(user.Address is not null)
            {
                user.Address.FirstName = address.FirstName;
                user.Address.LastName = address.LastName;
                user.Address.Street = address.Street;
                user.Address.City = address.City;
                user.Address.Country = address.Country;
            }
            else
            {
                var userAddress = _mapper.Map<Address>(address);
                user.Address = userAddress;
            }

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                throw new ValidationException(errors);
            }
            return address;

        }
        public async Task<UserResultDTo> LoginAsync(UserLoginDTo loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null)
                throw new UnAuthorizedException("Invalid Email Address");
            var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);
            if (!result)
                throw new UnAuthorizedException();
            return new UserResultDTo(user.DisplayName, loginModel.Email ,  await CreateTokenAsync(user));   
        }
        public async Task<UserResultDTo> RegisterAsync(UserRegisterDTo registerModel)
        {
            var user = new User()
            {
                DisplayName = registerModel.DisplayName,
                Email = registerModel.Email,
                PhoneNumber = registerModel.PhoneNumber,
                UserName = registerModel.UserName
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);  
                throw new ValidationException(errors);
            }

            return new UserResultDTo(user.DisplayName, registerModel.Email, await CreateTokenAsync(user));

        }
        private async Task<String> CreateTokenAsync(User user)
        {

            var JwtOptions = options.Value;

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: JwtOptions.Issuer,
                audience: JwtOptions.Audience,
                expires: DateTime.UtcNow.AddDays(JwtOptions.DurationInDays),
                claims: authClaims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<IEnumerable<string>> GetAllRolesasync()
        {
            var roles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            if (roles == null || !roles.Any())
                throw new Exception("No roles found");
            return roles;
        }
    }
}

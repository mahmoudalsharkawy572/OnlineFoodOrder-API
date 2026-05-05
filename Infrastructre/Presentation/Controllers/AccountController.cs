using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DataTransferObjects.IdentityDTos;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UserResultDTo>> Login(UserLoginDTo loginModel)
        {
            var result = await _serviceManager.AuthenticationService.LoginAsync(loginModel);
            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResultDTo>> Register(UserRegisterDTo registerModel)
        {
            var result = await _serviceManager.AuthenticationService.RegisterAsync(registerModel);
            return Ok(result);
        }

        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            var result = await _serviceManager.AuthenticationService.CheckEmailExist(email);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<UserResultDTo>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _serviceManager.AuthenticationService.GetUserByEmail(email);
            return Ok(user);
        }

        [HttpGet("address")]
        public async Task<ActionResult<AddressDTO>> GetUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await _serviceManager.AuthenticationService.GetUserAddress(email);
            return Ok(result);
        }

        [HttpPut("address")]
        public async Task<ActionResult<AddressDTO>> UpdateUserAddress(AddressDTO address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var result = await _serviceManager.AuthenticationService.UpdateUserAddress(address,email);
            return Ok(result);
        }

        [HttpGet("roles")]
        public async Task<ActionResult> GetAllRoles()
        {
            var result = await _serviceManager.AuthenticationService.GetAllRolesasync();
            return Ok(result);
        }

       
    }
}

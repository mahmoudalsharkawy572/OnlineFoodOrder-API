using Shared.DataTransferObjects.IdentityDTos;
using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IAuthenticationService
    {
        public Task<UserResultDTo> LoginAsync(UserLoginDTo loginModel);
        public Task<UserResultDTo> RegisterAsync(UserRegisterDTo registerModel);
        public Task<UserResultDTo> GetUserByEmail(string email);
        public Task<bool> CheckEmailExist(string email);
        public Task<AddressDTO> GetUserAddress(string email);
        public Task<AddressDTO> UpdateUserAddress(AddressDTO address, string email);
        public Task<IEnumerable<string>> GetAllRolesasync();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.IdentityDTos
{
    public record UserRegisterDTo
    {
        [Required(ErrorMessage = "Display Name is required.")]
        public string DisplayName { get; init; }
        
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; init; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; init; }

        [Required(ErrorMessage = "User Name is required.")]
        public string UserName { get; init; }

        [Required(ErrorMessage = "Phone Number is required.")]
        public string PhoneNumber { get; init; }


    }
}

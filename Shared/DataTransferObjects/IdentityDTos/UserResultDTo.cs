using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.IdentityDTos
{
    public record UserResultDTo(string DisplayName, string Email, string Token);

}

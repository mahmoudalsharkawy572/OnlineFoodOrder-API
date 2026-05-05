using Shared.DataTransferObjects.BasketModuleDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IPaymentService
    {
        public Task<BasketDTo> CreateOrUpdatePaymentIntent(string basketId);
        public Task UpdateOrderPaymentStatus(string request, string header);
    }
}

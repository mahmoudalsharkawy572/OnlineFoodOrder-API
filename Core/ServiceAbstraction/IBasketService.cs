using Shared.DataTransferObjects.BasketModuleDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IBasketService
    {
        public Task<BasketDTo> CreateOrUpdateBasketAsync(BasketDTo basket);

        public Task<BasketDTo> GetBasketAsync(string Key);

        public Task<bool> DeleteBasketAsync(string Key);
    }
}

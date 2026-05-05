using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServicesAbstraction;
using Shared.DataTransferObjects.BasketModuleDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BasketService(IBasketRepository _basketRepository,IMapper _mapper) : IBasketService
    {
        public async Task<BasketDTo> CreateOrUpdateBasketAsync(BasketDTo basket)
        {
            var CustomerBasket = _mapper.Map<BasketDTo, CustomerBasket>(basket);
            var CreatedOrUpdatedBasket = await _basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);
            if (CreatedOrUpdatedBasket is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("An error occurred while creating or updating the basket.");
        }

        public async Task<bool> DeleteBasketAsync(string Key) => await _basketRepository.DeleteBasketAsync(Key);

        public async Task<BasketDTo> GetBasketAsync(string Key)
        {
            var Basket= await _basketRepository.GetBasketAsync(Key);
            if (Basket is not null)
                return _mapper.Map<CustomerBasket, BasketDTo>(Basket);
            else
                throw new BasketNotFoundException(Key);
        }
    }
}

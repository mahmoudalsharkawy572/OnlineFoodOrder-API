using DomainLayer.Contracts;
using DomainLayer.Models.BasketModule;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StackExchange.Redis;
using System.Text.Json;


namespace Persistence.Repositories
{
    // To Make Connection with Redis we need to use IConnectionMultiplexer interface from StackExchange.Redis package
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        // We need to get the database from the connection to perform operations on it
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
        {
            // We need to serialize the basket object to a string before storing it in Redis
            var JsonBasket = JsonSerializer.Serialize(basket);

            // We need to set the serialized basket in Redis with the key as the basket Id and the value as the serialized basket
            var IsCreatedOrUpdated = await _database.StringSetAsync(basket.Id, JsonBasket, TimeToLive ?? TimeSpan.FromDays(30)); 
            if(IsCreatedOrUpdated)
                return await GetBasketAsync(basket.Id);
            else
                return null;
        }

        public async Task<bool> DeleteBasketAsync(string Key) => await _database.KeyDeleteAsync(Key);
      

        public async Task<CustomerBasket?> GetBasketAsync(string Key)
        {
            var Basket = await _database.StringGetAsync(Key);
            if (Basket.IsNullOrEmpty)
                return null;
            else
                return JsonSerializer.Deserialize<CustomerBasket>(Basket!);
        }
    }
}

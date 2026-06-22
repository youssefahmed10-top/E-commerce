using System.Runtime.InteropServices;
using System.Text.Json;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using StackExchange.Redis;

namespace Presistence.Repositories
{
    public class BasketRepository (IConnectionMultiplexer _connection): IBasketRepository
    {
        private readonly IDatabase _database = _connection.GetDatabase();
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket Basket, TimeSpan? TimeTolive = null)
        {
            var jsonBascket = JsonSerializer.Serialize(Basket);
            var result = await _database.StringSetAsync(Basket.Id, jsonBascket, TimeTolive ?? TimeSpan.FromDays(30));
                return result? await GetBasketAsync(Basket.Id) : null;
        }

        public async Task<bool> DeleteBasketAsync(string id)
            => await _database.KeyDeleteAsync(id);

        public async Task<CustomerBasket?> GetBasketAsync(string Id)
        {
             var result = await _database.StringGetAsync(Id);

            if (result.IsNullOrEmpty ) return null;
            return JsonSerializer.Deserialize<CustomerBasket>(result!);
        }





    }
}

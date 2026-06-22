using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.BasketModule;

namespace Domain.Contracts
{
    public interface IBasketRepository
    {
        //Create OR Update
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket Basket, TimeSpan? TimeTolive = null);

        //Delete 
        Task<bool> DeleteBasketAsync(string id);

        //GetBasketbyID
        Task<CustomerBasket?> GetBasketAsync(string Id);
    }
}

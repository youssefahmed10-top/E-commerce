using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Dtos.BasketModule;

namespace ServicesAbstraction.Contracts
{
    public interface IBasketService
    {
        //get
        Task<BasketDto> GetBasketAsync(string id);
        //CreateOrUpdate
        Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basketDto);
        //Delete
        Task<bool> DeleteBasketAsync(string id);
    }
}

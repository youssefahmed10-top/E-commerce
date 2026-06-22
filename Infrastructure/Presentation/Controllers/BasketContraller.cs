using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction.Contracts;
using Shared.Dtos.BasketModule;

namespace Presentation.Controllers
{
    [Authorize]
    public class BasketContraller (IServiceManager _serviceManager): ApiController
    {
        //get => GetDate
        [HttpGet]  //BaseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> GetBasketAsync(string id)
            => Ok(await _serviceManager.BasketService.GetBasketAsync(id));

        //post => CreateOrUpdate
        [HttpPost]  //BaseUrl/api/Basket
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basketDto)
            => Ok(await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basketDto));

        //Delete
        [HttpDelete("{id}")]  //BaseUrl/api/Basket/id
        public async Task<ActionResult> DeleteBasketAsync(string id)
        { 
            await _serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}

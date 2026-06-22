using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction.Contracts;
using Shared;
using Shared.Dtos;
using Shared.Enum;
using Shared.ErrorModels;

namespace Presentation.Controllers
{

    public class ProductsController (IServiceManager _serviceManager) : ApiController
    {
      
        //EndPoint ==> GetAllProduct
        [HttpGet] //BaseUrl/api/Products [Get]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProductsAsync([FromQuery]ProductSpecificationParameters parameters) 
            => Ok(await _serviceManager.ProductService.GetAllProductsAsync(parameters));

        //EndPoint ==> GetAllBrand
        [HttpGet("Brands")] //BaseUrl/api/product/Brands [Get]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrandsAsync()
            => Ok(await _serviceManager.ProductService.GetAllBrandsAsync());

        //EndPoint ==> GetAllType
        [HttpGet("Types")] //BaseUrl/api/Product/Types [Get]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAlltypesAsync()
            => Ok(await _serviceManager.ProductService.GetAllTypessAsync());




        [ProducesResponseType(typeof(ProductResultDto),StatusCodes.Status200OK)]

        
        //EndPoint ==> GetProductById
        [HttpGet("{id:int}")] //BaseUrl/api/Product/5 [Get]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
            => Ok(await _serviceManager.ProductService.GetProductByIdAsync(id));
    }
}

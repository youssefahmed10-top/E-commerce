using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.Dtos;
using Shared.Enum;

namespace ServicesAbstraction.Contracts
{
    public interface IProductService
    {
        //GetAllProducts
        Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters parameters);

        //GetAllBrands
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();

        //GetAllTypes
        Task<IEnumerable<TypeResultDto>> GetAllTypessAsync();

        //GetProductById
        Task<ProductResultDto> GetProductByIdAsync(int id);
    }
}

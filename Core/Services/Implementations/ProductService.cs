using System.Reflection.Metadata;
using AutoMapper;
using Domain.Contracts;
using Domain.Entities.ProductModel;
using Domain.Exceptions;
using Services.Specifications;
using ServicesAbstraction.Contracts;
using Shared;
using Shared.Dtos;
using Shared.Enum;

namespace Services.Implementations
{
    public class ProductService(IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();

            var BrandResult= _mapper.Map<IEnumerable<BrandResultDto>>(Brands);

            return BrandResult;
        }

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductsAsync(ProductSpecificationParameters parameters)
        {
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            var Specification = new ProductWithBrandAndTypeSpecifications(parameters);
            var Product = await ProductRepo.GetAllAsync(Specification);
            var productResult = _mapper.Map<IEnumerable<ProductResultDto>>(Product);

            var PageSize = productResult.Count();
            var CountSpecification = new ProductCountSpecification(parameters);
            var TotalCount = await ProductRepo.CountAsync(CountSpecification);
            return new PaginatedResult<ProductResultDto>(parameters.PageIndex, PageSize, TotalCount, productResult);
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypessAsync()
        {
            var Type =await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();

            var TypeResult = _mapper.Map<IEnumerable<TypeResultDto>>(Type);

            return TypeResult;
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            var Specification = new ProductWithBrandAndTypeSpecifications(id);
            var Products = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Specification);

            //var ProductResult = _mapper.Map<ProductResultDto>(Products);
            //return ProductResult;

            return Products is null ? throw new ProductNotFoundException(id) : _mapper.Map<ProductResultDto>(Products);
        }
    }
}

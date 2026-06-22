using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.ProductModel;
using Shared;
using Shared.Enum;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product,int>
    {
     //GetAllProduct =>[Include only]  
        public ProductWithBrandAndTypeSpecifications(ProductSpecificationParameters parameters) 
            :base(p =>(!parameters.Typeid.HasValue || p.TypeId == parameters.Typeid) &&
                      (!parameters.Brandid.HasValue || p.BrandId == parameters.Brandid)&&
                      (string.IsNullOrEmpty(parameters.Search) || p.Name.ToLower().Contains(parameters.Search.ToLower())))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (parameters.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    break;      
            }

            ApplyPagination(parameters.PageSize, parameters.PageIndex);

        }

        //GetProductById => [Expression of vInclude ,Expression of Where]
        public ProductWithBrandAndTypeSpecifications(int id) :base(p=>p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}

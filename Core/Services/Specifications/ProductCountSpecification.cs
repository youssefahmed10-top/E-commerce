using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities.ProductModel;
using Shared;

namespace Services.Specifications
{
    internal class ProductCountSpecification: BaseSpecifications<Product,int>
    {
        public ProductCountSpecification(ProductSpecificationParameters parameters) : base(p => (!parameters.Typeid.HasValue || p.TypeId == parameters.Typeid) &&
                      (!parameters.Brandid.HasValue || p.BrandId == parameters.Brandid) &&
                      (string.IsNullOrEmpty(parameters.Search) || p.Name.ToLower().Contains(parameters.Search.ToLower())))
        {

        }
    }
}

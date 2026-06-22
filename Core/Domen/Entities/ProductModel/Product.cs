using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.ProductModel
{
    public  class Product:BaseEntity<int>
    {
        public string Description { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;

        public decimal Price { get; set; }

        #region 1:M ProductBrand
        public ProductBrand ProductBrand { get; set; }  //Navgational prop
        public int BrandId { get; set; }              
        #endregion

        #region 1:M ProductType 
        public ProductType ProductType { get; set; }    //Navgational prop
        public int TypeId { get; set; }
        #endregion
    }
}

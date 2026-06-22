using System.ComponentModel;
using Shared.Enum;

namespace Shared
{
    public class ProductSpecificationParameters
    {
        private const int DefaultSize = 5;
        private const int MaxPageSize = 10;

        public int? Typeid { get; set; }
        public int? Brandid { get; set; }
        public ProductSortingOptions Sort { get; set; }
        public string? Search { get; set; }

        public int PageIndex { get; set; } = 1;  //number of page 

        private int _PageSize = DefaultSize;    //number of products in this page

        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value>MaxPageSize?MaxPageSize:value; }
        }

    }
}

using DomainLayer.Models.ProductModule;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product , int>
    {
        

        // Get All Products With Brands And Types (You can choose Products with same brand , type or both)
        public ProductWithBrandAndTypeSpecifications(ProductQueryParams queryParams) 
            : base( P => (!queryParams.BrandId.HasValue || P.BrandId == queryParams.BrandId) 
               && (!queryParams.TypeId.HasValue || P.TypeId == queryParams.TypeId) 
               && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || P.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
            
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
          
            switch(queryParams.SortingOption)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(P => P.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDescending(P => P.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(P => P.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDescending(P => P.Price);
                    break;
                default:
                    break;

            }
           
            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }

        // Get Product With Specific Id
        public ProductWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}

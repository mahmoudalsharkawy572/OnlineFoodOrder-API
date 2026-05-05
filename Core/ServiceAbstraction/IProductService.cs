using Shared;
using Shared.DataTransferObjects.ProductModuleDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IProductService
    {
        // Get All Products
        Task<PaginatedResult<ProductDTo>> GetAllProductsAsync(ProductQueryParams queryParams);

        // Get Product By Id
        Task<ProductDTo> GetProductByIdAsync(int id);

        // Get All Brands
        Task<IEnumerable<BrandDTo>> GetAllBrandsAsync();

        // Get All types
        Task<IEnumerable<TypeDTo>> GetAllTypesAsync();
    }
}

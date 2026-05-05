using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared;
using Shared.DataTransferObjects.ProductModuleDTos;


namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")] // BaseUrl/api/Products
    public class ProductsController(IServiceManager _serivceManager) : ControllerBase
    {
        // Get All Products
        [HttpGet]
        public  async Task<ActionResult<PaginatedResult<ProductDTo>>>GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var Products = await _serivceManager.ProductService.GetAllProductsAsync(queryParams);
            return Ok(Products);
        }

        // Get Product By Id
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTo>>GetProduct(int id)
        {
            var Product = await _serivceManager.ProductService.GetProductByIdAsync(id);
            if(Product is not null)
                return Ok(Product);
            return BadRequest();
        }

        //Get All Brands
        [HttpGet("Brands")]
        public async Task<ActionResult<BrandDTo>> GetAllBrands()
        {
            var Brands = await _serivceManager.ProductService.GetAllBrandsAsync();
            return Ok(Brands);
        }

        //Get All Types
        [HttpGet("Types")]
        public async Task<ActionResult<TypeDTo>> GetAllTypes()
        {
            var Types =  await _serivceManager.ProductService.GetAllTypesAsync();
            return Ok(Types);
        }
    }
}

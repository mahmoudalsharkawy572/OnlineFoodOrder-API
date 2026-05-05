using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.ProductModule;
using Services.Specifications;
using ServicesAbstraction;
using Shared;
using Shared.DataTransferObjects.ProductModuleDTos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDTo>> GetAllBrandsAsync()
        {

            var Repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var Brands = await Repo.GetAllAsync();
            var BrandsDTo = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDTo>>(Brands);
            return BrandsDTo;
        }

        public async Task<PaginatedResult<ProductDTo>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(queryParams);
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var Products = await Repo.GetAllAsync(specifications);
            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTo>>(Products);
            var count = Products.Count();
            var CountSpec = new ProductCountSpecification(queryParams);
            var TotalCount = await Repo.CountAsync(CountSpec);
            return new PaginatedResult<ProductDTo> (queryParams.PageIndex, count, TotalCount, Data);
        }

        public async Task<IEnumerable<TypeDTo>> GetAllTypesAsync()
        {
            var Repo = _unitOfWork.GetRepository<ProductType, int>();
            var Types = await Repo.GetAllAsync();
            var TypesDTo = _mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDTo>>(Types);
            return TypesDTo;

        }

        public async Task<ProductDTo> GetProductByIdAsync(int id)
        {
            var specifications = new ProductWithBrandAndTypeSpecifications(id);
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var Product = await Repo.GetByIdAsync(specifications);
            if (Product is not null)
                return _mapper.Map<Product, ProductDTo>(Product);
            else
                throw new ProductNotFoundException(id);
        }
    }
}

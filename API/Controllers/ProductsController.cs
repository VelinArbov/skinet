using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
 
    public class ProductsController : BaseApiController
    {

        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductType> _typeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> brandsRepo,
        IGenericRepository<ProductType> typeRepo, IMapper mapper)
        {
            _mapper = mapper;
            _typeRepo = typeRepo;
            _brandsRepo = brandsRepo;
            _productsRepo = productsRepo;


        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery]ProductsSpecParamas productParams)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(productParams);

            var countSPec = new ProductWithFiltersForCountSpecification(productParams);


            var totalItems = await _productsRepo.CountAsync(countSPec);


            var products = await _productsRepo.ListAsync(spec);


            var data = (_mapper.Map<IReadOnlyList<Product>,
            IReadOnlyList<ProductToReturnDto>>(products));
            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,productParams.PageSize, totalItems, data));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var spec = new ProductWithTypesAndBrandsSpecification(id);

            var product = await _productsRepo.GetEntityWithSpec(spec);

            if(product == null) return NotFound(new ApiResponse(404));

            return _mapper.Map<Product,ProductToReturnDto>(product);
           


        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var productsBrands = await _brandsRepo.GetAllAsync();
            return Ok(productsBrands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var productTypes = await _typeRepo.GetAllAsync();
            return Ok(productTypes);
        }


    }
}
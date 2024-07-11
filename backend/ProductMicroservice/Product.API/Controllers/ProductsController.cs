using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.API.Models;
using Product.API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Product.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<DtoProduct>>> GetProductsAsync()
        {
            var products = await _productService.GetProductsAsync();
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ActionName(nameof(GetProductByIdAsync))]
        public async Task<ActionResult<DtoProduct>> GetProductByIdAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpGet("/Products/Name")]
        public async Task<ActionResult<List<DtoProduct>>> GetProductsByNameAsync(string nameFragment)
        {
            var products = await _productService.GetProductsByNameAsync(nameFragment);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult<DtoProduct>> PostProductAsync(DtoProduct dtoProduct)
        {
            var createdProduct = await _productService.PostProductAsync(dtoProduct);
            if (createdProduct == null)
            {
                return BadRequest();
            }
            return CreatedAtAction(nameof(GetProductByIdAsync), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductAsync(int id, DtoProduct dtoProduct)
        {
            if (id != dtoProduct.Id)
            {
                return BadRequest();
            }
            if (!await  ProductExistsAsync(id)) {
                return NotFound();
            }
            await _productService.PutProductAsync(dtoProduct);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            if (!await ProductExistsAsync(id))
            {
                return NotFound();
            }   
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        private async Task<bool> ProductExistsAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return product != null;
        }
    }
}
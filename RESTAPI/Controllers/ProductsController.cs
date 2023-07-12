using Microsoft.AspNetCore.Mvc;
using RESTAPI.Models;
using RESTAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;  
        }

        // GET: api/ProductsController
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        // GET api/ProductsController/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get( string id)
        {
            var product = await _productService.GetById(id);

            if (product != null)
            {
                return Ok(product);
            }
            return NotFound();
        }

        // POST api/ProductsController
        [HttpPost]
        public async Task<IActionResult> Post(Product product)
        {
            await _productService.CreateAsync(product);
            if(product != null) {
                return Ok(product);
            }
            return NotFound();
        }

        // PUT api/ProductsController/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Product Newproduct)
        {
            var checkExistance = await _productService.GetById(Newproduct.Id);
            if (checkExistance == null) {
                return NotFound();
            }
            await _productService.Update(id, Newproduct);
            return Ok(Newproduct);

            
            
        }

        // DELETE api/ProductsController/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var checkExistance = await _productService.GetById(id);
            if (checkExistance == null)
            {
                return NotFound();
            }
            await _productService.DeleteAsync(id);
            return Ok();
        }
    }
}

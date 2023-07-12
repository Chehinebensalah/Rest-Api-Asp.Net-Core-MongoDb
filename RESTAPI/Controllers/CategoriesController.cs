using Microsoft.AspNetCore.Mvc;
using RESTAPI.Models;
using RESTAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;  
        }

        // GET: api/CategoriesController
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        // GET api/CategoriesController/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get( string id)
        {
            var category = await _categoryService.GetById(id);

            if (category != null)
            {
                return Ok(category);
            }
            return NotFound();
        }

        // POST api/CategoriesController
        [HttpPost]
        public async Task<IActionResult> Post(Category category)
        {
            await _categoryService.CreateAsync(category);
            if(category != null) {
                return Ok(category);
            }
            return NotFound();
        }

        // PUT api/CategoriesController/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Category Newcategory)
        {
            var checkExistance = await _categoryService.GetById(Newcategory.Id);
            if (checkExistance == null) {
                return NotFound();
            }
            await _categoryService.Update(id, Newcategory);
            return Ok(Newcategory);

            
            
        }

        // DELETE api/CategoriesController/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var checkExistance = await _categoryService.GetById(id);
            if (checkExistance == null)
            {
                return NotFound();
            }
            await _categoryService.DeleteAsync(id);
            return Ok();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRecipes()
        {
            string[] recipes = { "Pizza", "Curry", "Oxtail"};
            if (!recipes.Any())
                return NotFound();
            return Ok(recipes);
        }

        [HttpPost]
        public IActionResult CreateRecipe()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Created("", new {});
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRecipes(int id) 
        {
            bool badThingsHappened = false;
            if (badThingsHappened)
                return BadRequest();

            return NoContent();
        }

        [HttpPut]
        public IActionResult EditRecipe()
        {
            if (!ModelState.IsValid)
                return NotFound();

            return NoContent();
        }
    }
}

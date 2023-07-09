using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRecipes([FromQuery] int count)
        {
            Recipe[] recipes = {
                new Recipe() {Title = "Pizza"},
                new Recipe() {Title = "Curry"},
                new Recipe() {Title = "Oxtail"}
            };
            if (!recipes.Any())
                return NotFound();
            return Ok(recipes.Take(count));
        }

        [HttpPost]
        public IActionResult CreateRecipe([FromBody] Recipe newRecipe)
        {
            //validate and then save data to the database
            bool badThingsHappened = false;
            if (badThingsHappened)
                return BadRequest();

            return Created("", newRecipe);
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Mime;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RecipeServiceMongoDB _recipeService;
        public RecipesController(RecipeServiceMongoDB recipeService)
        {
            _recipeService = recipeService;
        }
        /// <summary>
        ///  Returns all available recipes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRecipes()
        {
            var recipes = await _recipeService.GetRandomAsync();
            if (!recipes.Any())
                return NotFound();
            return Ok(recipes);
        }
        /// <summary>
        /// Returns a recipe for a given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(string id)
        {
            if (id.Length < 24)
            {
                throw new ArgumentException("length of id should be 24");
            }
            var recipe = await _recipeService.GetAsync(id);

            if (recipe is null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }

        /// <summary>
        /// Creates a new recipe
        /// </summary>
        /// <param name="newRecipe"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] Recipe newRecipe)
        {
            //validate and then save data to the database
            if (!ModelState.IsValid)
                return BadRequest();

            await _recipeService.CreateAsync(newRecipe);

            return Created("", newRecipe);
        }

        /// <summary>
        /// Removes a recipe with the given id if matched
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(string id)
        {
            var recipe = _recipeService.GetAsync(id);
            if (recipe is null)
            {
                return NotFound();
            }
            await _recipeService.RemoveAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Updates a recipe
        /// </summary>
        /// <param name="updatedRecipe"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> EditRecipe([FromBody] Recipe updatedRecipe)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var recipe = _recipeService.GetAsync(updatedRecipe.Id);
            if (recipe is null)
                return NotFound();

            await _recipeService.UpdateAsync(updatedRecipe.Id, updatedRecipe);
            return NoContent();
        }

        /// <summary>
        /// Updates a specific attribute of a recipe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recipeUpdates"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public async Task<IActionResult> EditRecipe(string id, JsonPatchDocument<Recipe> recipeUpdates)
        {  
            var recipe = await _recipeService.GetAsync(id);
            if (recipe is null)
                return NotFound(); 
            recipeUpdates.ApplyTo(recipe);
            await _recipeService.UpdateAsync(id, recipe);
            return NoContent();
        }
    }
}

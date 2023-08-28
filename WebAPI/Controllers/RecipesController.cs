using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly RecipeServiceMongoDB _recipeService;
        public RecipesController(RecipeServiceMongoDB recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            var recipes = await _recipeService.GetRandomAsync();
            if (!recipes.Any())
                return NotFound();
            return Ok(recipes);
        }

        [HttpGet("Id")]
        public async Task<ActionResult<Recipe>> GetRecipe(string id)
        {
            var recipe = await _recipeService.GetAsync(id);

            if (recipe is null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeCreateVM recipeVM)
        {
            //validate and then save data to the database
            if (!ModelState.IsValid)
                return BadRequest();

            Recipe recipe = new() 
            {
              Created = recipeVM.Created,
              Description = recipeVM.Description,
              Directions = recipeVM.Directions,
              Ingredients = recipeVM.Ingredients,
              RecipeId = recipeVM.RecipeId,
              Tags = recipeVM.Tags,
              Title = recipeVM.Title
            };


            await _recipeService.CreateAsync(recipe);

            return Created("", recipe);
        }

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRecipe(string id) 
        //{
        //    var recipe = _recipeService.GetAsync(id);
        //    if (recipe is null)
        //    {
        //        return NotFound();
        //    }
        //    await _recipeService.RemoveAsync(id);
        //    return NoContent();
        //}

        //[HttpPut]
        //public async Task<IActionResult> EditRecipe([FromBody] Recipe updatedRecipe)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var recipe = _recipeService.GetRandomAsync(updatedRecipe._Id);
        //    if (recipe is null)
        //        return NotFound();

        //    await _recipeService.UpdateAsync(updatedRecipe._Id, updatedRecipe);
        //    return NoContent();
        //}
    }
}

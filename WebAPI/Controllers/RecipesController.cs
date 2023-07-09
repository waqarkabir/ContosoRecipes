using Microsoft.AspNetCore.Http;
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
            if (recipes.Any())
                return NotFound();
            return Ok(recipes);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        [HttpGet]
        public string[] GetRecipes()
        {
            string[] recipes = { "Pizza", "Curry", "Oxtail"};
            return recipes;
        }
    }
}

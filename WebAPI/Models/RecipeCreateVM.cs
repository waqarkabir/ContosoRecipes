using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace WebAPI.Models
{
 
    public class RecipeCreateVM
	{
        public string RecipeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Directions { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
    }


}

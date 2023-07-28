namespace WebAPI.Models
{
    public class RecipeCollectionDatabaseSettings
    {
        public string ConnectionString { get; set; } = null;
        public string DatabaseName { get; set; } = null;
        public string RecipesCollectionName { get; set; } = null;
    }
}

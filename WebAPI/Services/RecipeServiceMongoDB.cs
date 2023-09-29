using WebAPI.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace WebAPI.Services;

public class RecipeServiceMongoDB
{
    private readonly IMongoCollection<Recipe> _recipesCollection;

    public RecipeServiceMongoDB(IOptions<RecipeCollectionDatabaseSettings> recipeCollectionDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            recipeCollectionDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            recipeCollectionDatabaseSettings.Value.DatabaseName);

        _recipesCollection = mongoDatabase.GetCollection<Recipe>(
            recipeCollectionDatabaseSettings.Value.RecipesCollectionName);
    }

    public async Task<List<Recipe>> GetRandomAsync() =>
        await _recipesCollection.Find(_ => true)
        //.Limit(count)
        .ToListAsync();

    public async Task<Recipe?> GetAsync(string id) =>
        await _recipesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Recipe newRecipe) =>
        await _recipesCollection.InsertOneAsync(newRecipe);

    public async Task UpdateAsync(string id, Recipe updatedRecipe) =>
        await _recipesCollection.ReplaceOneAsync(x => x.Id == id, updatedRecipe);

    public async Task RemoveAsync(string id) =>
        await _recipesCollection.DeleteOneAsync(x => x.Id == id);
}
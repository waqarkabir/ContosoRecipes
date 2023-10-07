
using Microsoft.OpenApi.Models;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson();
            //builder.Services.AddMongoDb(builder.Configuration.GetSection("MongoDatabase"));
            builder.Services.Configure<RecipeCollectionDatabaseSettings>(
            builder.Configuration.GetSection("MongoDatabase"));

            builder.Services.AddSingleton<RecipeServiceMongoDB>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ContosoRecipes" , Version = "v1"});
            }).AddSwaggerGenNewtonsoftSupport();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContosoRecipes v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
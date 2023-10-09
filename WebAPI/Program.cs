
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
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
                c.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Title = "Contoso Recipe API" ,
                    Description = "Sample ASP.Net Core Web API that allows you work with recipe data",
                    Contact = new OpenApiContact 
                    {
                        Name = "Code With Mind",
                        Url = new Uri("https://www.codewithmind.com")
                    },
                    Version = "v1"
                });

                //generate the xml docs that'll drive the swagger docs
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory,xmlFile);

                c.IncludeXmlComments(xmlPath);

                c.CustomOperationIds(apidescription =>
                {
                    return apidescription.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null;
                });

            }).AddSwaggerGenNewtonsoftSupport();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContosoRecipes v1");
                    c.DisplayOperationId();
                });
            }
            else { 
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
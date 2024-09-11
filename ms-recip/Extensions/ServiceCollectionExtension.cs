using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repositories.CategoriesRepository;
using ms_recip.Repositories.IngredientQuantitiesRepository;
using ms_recip.Repositories.IngredientsRepository;
using ms_recip.Repositories.ProfilsRepository;
using ms_recip.Repositories.RecipsRepository;
using ms_recip.Repositories.RecipStepsRepository;
using ms_recip.Services.LoggerService;
using System.Text.Json.Serialization;


namespace ms_recip.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlite(connectionString));
    }

    public static void AddODataContext(this IServiceCollection services)
    {
        var modelBuilder = new ODataConventionModelBuilder();
        modelBuilder.EntitySet<RecipModel>(nameof(DatabaseContext.Recips));
        modelBuilder.EntitySet<IngredientModel>(nameof(DatabaseContext.Ingredients));
        modelBuilder.EntitySet<CategoryModel>(nameof(DatabaseContext.Categories));
        modelBuilder.EntitySet<ProfilModel>(nameof(DatabaseContext.Profils));
        modelBuilder.EntitySet<RecipStepModel>(nameof(DatabaseContext.RecipSteps));

        services.AddControllers()
            .AddOData(
                options => options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100).AddRouteComponents(
                    "odata",
                    modelBuilder.GetEdmModel())
            )
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
    }

    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddTransient<ILogger, LoggerService>();
    }
    
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IProfilsRepository, ProfilsRepository>();
        services.AddTransient<ICategoriesRepository, CategoriesRepository>();
        services.AddTransient<IIngredientsRepository, IngredientsRepository>();
        services.AddTransient<IRecipsRepository, RecipsRepository>();
        services.AddTransient<IRecipStepsRepository, RecipStepsRepository>();
        services.AddTransient<IIngredientQuantitiesRepository, IngredientQuantitiesRepository>();
    }
}

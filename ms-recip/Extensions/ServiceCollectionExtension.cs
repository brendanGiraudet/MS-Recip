using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using ms_recip.Data;
using ms_recip.Models;
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
}

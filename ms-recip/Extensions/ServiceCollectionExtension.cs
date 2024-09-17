using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repositories.CategoriesRepository;
using ms_recip.Repositories.IngredientQuantitiesRepository;
using ms_recip.Repositories.IngredientsRepository;
using ms_recip.Repositories.ProfilCategoriesRepository;
using ms_recip.Repositories.ProfilsRepository;
using ms_recip.Repositories.RecipCalendarsRepository;
using ms_recip.Repositories.RecipCategoriesRepository;
using ms_recip.Repositories.RecipsRepository;
using ms_recip.Repositories.RecipStepsRepository;
using ms_recip.Repositories.UsersRepository;
using ms_recip.Services.ConfigurationService;
using ms_recip.Services.LoggerService;
using ms_recip.Services.RabbitMq;
using ms_recip.Services.RabbitMqProducerService;
using ms_recip.Settings;
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
        modelBuilder.EntitySet<UserModel>(nameof(DatabaseContext.Users));
        modelBuilder.EntitySet<RecipCalendarModel>(nameof(DatabaseContext.RecipCalendars));

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
        services.AddTransient<IConfigurationService, ConfigurationService>();
        services.AddTransient<IRabbitMqProducerService, RabbitMqProducerService>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IProfilsRepository, ProfilsRepository>();
        services.AddTransient<ICategoriesRepository, CategoriesRepository>();
        services.AddTransient<IIngredientsRepository, IngredientsRepository>();
        services.AddTransient<IRecipStepsRepository, RecipStepsRepository>();
        services.AddTransient<IIngredientQuantitiesRepository, IngredientQuantitiesRepository>();
        services.AddTransient<IRecipCategoriesRepository, RecipCategoriesRepository>();
        services.AddTransient<IProfilCategoriesRepository, ProfilCategoriesRepository>();
        services.AddTransient<IUsersRepository, UsersRepository>();
        services.AddTransient<IRecipCalendarsRepository, RecipCalendarsRepository>();
        services.AddTransient<IRecipsRepository, RecipsRepository>();
    }

    public static void AddCustomHostedServices(this IServiceCollection services)
    {
        services.AddHostedService<RabbitMqSubscriberService>();
    }
    
    public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MSConfigurationSettings>(configuration.GetSection("MSConfigurationSettings"));
    }
}

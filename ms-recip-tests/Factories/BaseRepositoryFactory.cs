using Microsoft.Extensions.Logging;
using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repositories.BaseRepository;
using ms_recip.Repositories.CategoriesRepository;
using ms_recip.Repositories.IngredientsRepository;

namespace ms_recip_tests.Factories;

public static class BaseRepositoryFactory<T>
{
    public static IBaseRepository<T> CreateBaseRepository(DatabaseContext databaseContext, ILogger logger)
    {
        var typeName = typeof(T).Name;

        var baseRepository = typeName switch
        {
            nameof(CategoryModel) => new CategoriesRepository(databaseContext, logger) as IBaseRepository<T>,
            nameof(IngredientModel) => new IngredientsRepository(databaseContext, logger) as IBaseRepository<T>,
            _ => null
        };

        return baseRepository;
    }
}

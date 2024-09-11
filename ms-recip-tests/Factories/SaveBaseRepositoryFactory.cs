using Microsoft.Extensions.Logging;
using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repositories.IngredientQuantitiesRepository;
using ms_recip.Repositories.SaveBaseRepository;

namespace ms_recip_tests.Factories;

public static class SaveBaseRepositoryFactory<T>
{
    public static ISaveBaseRepository<T> CreateSaveBaseRepository(DatabaseContext databaseContext, ILogger logger)
    {
        var typeName = typeof(T).Name;

        var baseRepository = typeName switch
        {
            nameof(IngredientQuantityModel) => new IngredientQuantitiesRepository(databaseContext, logger) as ISaveBaseRepository<T>,
            _ => null
        };

        return baseRepository;
    }
}

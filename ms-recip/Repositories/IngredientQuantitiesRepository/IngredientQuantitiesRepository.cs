using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repositories.SaveBaseRepository;

namespace ms_recip.Repositories.IngredientQuantitiesRepository;

public class IngredientQuantitiesRepository(
        DatabaseContext databaseContext,
        ILogger logger)
    : SaveBaseRepository<IngredientQuantityModel>(
        databaseContext,
        logger,
        databaseContext.IngredientQuantities),
    IIngredientQuantitiesRepository
{
}

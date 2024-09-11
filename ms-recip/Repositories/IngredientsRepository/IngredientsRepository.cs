using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repositories.BaseRepository;

namespace ms_recip.Repositories.IngredientsRepository;

public class IngredientsRepository(
    DatabaseContext databaseContext,
    ILogger logger) : BaseRepository<IngredientModel>(databaseContext, logger, databaseContext.Ingredients), IIngredientsRepository
{
}

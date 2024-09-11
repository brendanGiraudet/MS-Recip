using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repository.BaseRepository;

namespace ms_recip.Repository.IngredientsRepository;

public class IngredientsRepository(
    DatabaseContext databaseContext,
    ILogger logger) : BaseRepository<IngredientModel>(databaseContext, logger, databaseContext.Ingredients), IIngredientsRepository
{
}

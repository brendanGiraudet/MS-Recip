using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repository.BaseRepository;

namespace ms_recip.Repository.IngredientsRepository;

public class IngredientsRepository : BaseRepository<IngredientModel>, IIngredientsRepository
{
    public IngredientsRepository(
        DatabaseContext databaseContext,
        ILogger logger)
        : base(databaseContext, logger, databaseContext.Ingredients)
    {
    }
}

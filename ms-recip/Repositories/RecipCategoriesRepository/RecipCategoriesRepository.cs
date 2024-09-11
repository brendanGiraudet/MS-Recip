using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repositories.SaveBaseRepository;

namespace ms_recip.Repositories.RecipCategoriesRepository;

public class RecipCategoriesRepository(
        DatabaseContext databaseContext,
        ILogger logger)
    : SaveBaseRepository<RecipCategoryModel>(
        databaseContext,
        logger,
        databaseContext.RecipCategories),
    IRecipCategoriesRepository
{
}

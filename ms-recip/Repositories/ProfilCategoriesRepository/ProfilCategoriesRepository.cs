using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repositories.SaveBaseRepository;

namespace ms_recip.Repositories.ProfilCategoriesRepository;

public class ProfilCategoriesRepository(
        DatabaseContext databaseContext,
        ILogger logger)
    : SaveBaseRepository<ProfilCategoryModel>(
        databaseContext,
        logger,
        databaseContext.ProfilCategories),
    IProfilCategoriesRepository
{
}

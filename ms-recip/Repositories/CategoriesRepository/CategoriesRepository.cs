using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repositories.BaseRepository;

namespace ms_recip.Repositories.CategoriesRepository;

public class CategoriesRepository(
    DatabaseContext databaseContext,
    ILogger logger) : BaseRepository<CategoryModel>(databaseContext, logger, databaseContext.Categories), ICategoriesRepository
{
}

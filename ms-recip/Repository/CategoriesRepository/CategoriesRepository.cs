using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repository.BaseRepository;

namespace ms_recip.Repository.CategoriesRepository;

public class CategoriesRepository : BaseRepository<CategoryModel>, ICategoriesRepository
{
    public CategoriesRepository(
        DatabaseContext databaseContext,
        ILogger logger)
        : base(databaseContext, logger, databaseContext.Categories)
    {
    }
}

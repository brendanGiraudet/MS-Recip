using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repositories.BaseRepository;

namespace ms_recip.Repositories.RecipsRepository;

public class RecipsRepository(
    DatabaseContext databaseContext,
    ILogger logger) : BaseRepository<RecipModel>(databaseContext, logger, databaseContext.Recips), IRecipsRepository
{
}

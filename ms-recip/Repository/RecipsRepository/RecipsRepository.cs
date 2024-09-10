using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repository.BaseRepository;

namespace ms_recip.Repository.RecipsRepository;

public class RecipsRepository : BaseRepository<RecipModel>, IRecipsRepository
{
    public RecipsRepository(
        DatabaseContext databaseContext,
        ILogger logger)
        : base(databaseContext, logger, databaseContext.Recips)
    {
    }
}

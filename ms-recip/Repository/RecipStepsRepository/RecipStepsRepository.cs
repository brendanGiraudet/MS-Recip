using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repository.BaseRepository;

namespace ms_recip.Repository.RecipStepsRepository;

public class RecipStepsRepository : BaseRepository<RecipStepModel>, IRecipStepsRepository
{
    public RecipStepsRepository(
        DatabaseContext databaseContext,
        ILogger logger)
        : base(databaseContext, logger, databaseContext.RecipSteps)
    {
    }
}

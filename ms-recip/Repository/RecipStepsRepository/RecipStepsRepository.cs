using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repository.BaseRepository;

namespace ms_recip.Repository.RecipStepsRepository;

public class RecipStepsRepository(
    DatabaseContext databaseContext,
    ILogger logger) : BaseRepository<RecipStepModel>(databaseContext, logger, databaseContext.RecipSteps), IRecipStepsRepository
{
}

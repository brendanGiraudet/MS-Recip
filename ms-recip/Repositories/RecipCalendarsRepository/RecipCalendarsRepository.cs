using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repositories.BaseRepository;

namespace ms_recip.Repositories.RecipCalendarsRepository;

public class RecipCalendarsRepository(
    DatabaseContext databaseContext,
    ILogger logger) : BaseRepository<RecipCalendarModel>(databaseContext, logger, databaseContext.RecipCalendars), IRecipCalendarsRepository
{
}

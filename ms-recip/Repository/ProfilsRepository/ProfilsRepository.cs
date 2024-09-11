using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repository.BaseRepository;

namespace ms_recip.Repository.ProfilsRepository;

public class ProfilsRepository(
    DatabaseContext databaseContext,
    ILogger logger) : BaseRepository<ProfilModel>(databaseContext, logger, databaseContext.Profils), IProfilsRepository
{
}

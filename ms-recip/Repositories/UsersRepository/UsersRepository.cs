using ms_recip.Data;
using ms_recip.Models;
using ms_recip.Repositories.BaseRepository;

namespace ms_recip.Repositories.UsersRepository;

public class UsersRepository(
    DatabaseContext databaseContext,
    ILogger logger) : BaseRepository<UserModel>(databaseContext, logger, databaseContext.Users), IUsersRepository
{
}

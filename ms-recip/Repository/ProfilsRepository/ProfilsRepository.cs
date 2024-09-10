using Microsoft.EntityFrameworkCore;
using ms_recip.Data;
using ms_recip.Models;

namespace ms_recip.Repository.ProfilsRepository;

public class ProfilsRepository(
    DatabaseContext databaseContext,
    ILogger logger) : IProfilsRepository
{
    private readonly DatabaseContext _databaseContext = databaseContext;
    private readonly ILogger _logger = logger;

    /// <inheritdoc/>
    public MethodResult<IQueryable<ProfilModel>> GetProfils()
    {
        try
        {
            var profils = _databaseContext.Profils.AsQueryable();

            return MethodResult<IQueryable<ProfilModel>>.CreateSuccessResult(profils);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            return MethodResult<IQueryable<ProfilModel>>.CreateErrorResult(ex.Message);
        }
    }

    /// <inheritdoc/>
    public async Task<MethodResult<ProfilModel>> GetProfilAsync(Guid id)
    {
        try
        {
            var profil = await _databaseContext.Profils.FirstOrDefaultAsync(p => p.Id == id);

            return MethodResult<ProfilModel>.CreateSuccessResult(profil);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            return MethodResult<ProfilModel>.CreateErrorResult(ex.Message);
        }
    }

    /// <inheritdoc/>
    public async Task<MethodResult<ProfilModel>> CreateProfilAsync(ProfilModel model)
    {
        try
        {
            _databaseContext.Profils.Add(model);

            await _databaseContext.SaveChangesAsync();

            return MethodResult<ProfilModel>.CreateSuccessResult(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            return MethodResult<ProfilModel>.CreateErrorResult(ex.Message);
        }
    }

    public async Task<MethodResult<ProfilModel>> UpdateProfilAsync(Guid id, ProfilModel model)
    {
        try
        {
            var actualIngredient = _databaseContext.Profils.AsNoTracking().FirstOrDefault(c => c.Id == id);

            if (actualIngredient == null || actualIngredient.Id != model.Id) return MethodResult<ProfilModel>.CreateSuccessResult(model);

            _databaseContext.Profils.Update(model);

            await _databaseContext.SaveChangesAsync();

            return MethodResult<ProfilModel>.CreateSuccessResult(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            return MethodResult<ProfilModel>.CreateErrorResult(ex.Message);
        }
    }

    public async Task<MethodResult<bool>> DeleteProfilAsync(Guid id)
    {
        try
        {
            var actualIngredient = _databaseContext.Profils.FirstOrDefault(c => c.Id == id);

            if (actualIngredient == null) return MethodResult<bool>.CreateSuccessResult(false);

            actualIngredient.Deleted = true;

            _databaseContext.Profils.Update(actualIngredient);

            await _databaseContext.SaveChangesAsync();

            return MethodResult<bool>.CreateSuccessResult(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            return MethodResult<bool>.CreateErrorResult(ex.Message);
        }
    }
}

using ms_recip.Models;

namespace ms_recip.Repository.ProfilsRepository;

public interface IProfilsRepository
{
    /// <summary>
    /// Get entire profils
    /// </summary>
    /// <returns></returns>
    MethodResult<IQueryable<ProfilModel>> GetProfils();

    /// <summary>
    /// Get only one profil by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<MethodResult<ProfilModel>> GetProfilAsync(Guid id);

    /// <summary>
    /// Create a profil
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<MethodResult<ProfilModel>> CreateProfilAsync(ProfilModel model);
    
    /// <summary>
    /// Update a profil
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<MethodResult<ProfilModel>> UpdateProfilAsync(Guid id, ProfilModel model);
    
    /// <summary>
    /// Delete a profil
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<MethodResult<bool>> DeleteProfilAsync(Guid id);
}

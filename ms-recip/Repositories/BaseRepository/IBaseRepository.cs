using ms_recip.Models;
using ms_recip.Repositories.GetBaseRepository;
using System.Linq.Expressions;

namespace ms_recip.Repositories.BaseRepository;

public interface IBaseRepository<T> : IGetBaseRepository<T>
{
    /// <summary>
    /// Create a Item
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<MethodResult<T>> CreateItemAsync(T model);

    /// <summary>
    /// Update a Item with existant verification 
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<MethodResult<T>> UpdateItemAsync(Expression<Func<T,bool>> filterExpression, T model);

    /// <summary>
    /// Update a Item
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<MethodResult<T>> UpdateItemAsync(T model);

    /// <summary>
    /// Delete a Item with existant verification
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<MethodResult<bool>> DeleteItemAsync(Expression<Func<T,bool>> filterExpression);

    /// <summary>
    /// Delete a Item
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<MethodResult<bool>> DeleteItemAsync(T model);
}

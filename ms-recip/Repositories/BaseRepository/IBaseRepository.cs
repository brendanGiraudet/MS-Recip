using ms_recip.Models;
using System.Linq.Expressions;

namespace ms_recip.Repositories.BaseRepository;

public interface IBaseRepository<T>
{
    /// <summary>
    /// Get entire items
    /// </summary>
    /// <returns></returns>
    MethodResult<IQueryable<T>> GetItems();

    /// <summary>
    /// Get only one Item by filter expression
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<MethodResult<T>> GetItemAsync(Expression<Func<T,bool>> filterExpression);

    /// <summary>
    /// Create a Item
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<MethodResult<T>> CreateItemAsync(T model);
    
    /// <summary>
    /// Update a Item
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<MethodResult<T>> UpdateItemAsync(Expression<Func<T,bool>> filterExpression, T model);
    
    /// <summary>
    /// Delete a Item
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<MethodResult<bool>> DeleteItemAsync(Expression<Func<T,bool>> filterExpression);
}

using System;
using System.Linq.Expressions;
using ms_recip.Models;

namespace ms_recip.Repositories.GetBaseRepository;

public interface IGetBaseRepository<T>
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
    Task<MethodResult<T>> GetItemAsync(Expression<Func<T, bool>> filterExpression);
}

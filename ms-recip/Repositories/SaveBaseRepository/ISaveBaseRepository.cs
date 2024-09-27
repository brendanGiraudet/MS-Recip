using ms_recip.Models;
using ms_recip.Repositories.GetBaseRepository;
using System.Linq.Expressions;

namespace ms_recip.Repositories.SaveBaseRepository;

public interface ISaveBaseRepository<T> : IGetBaseRepository<T>
{
    /// <summary>
    /// Save items is remove and add items if necessary
    /// </summary>
    /// <param name="expectedItems"></param>
    /// <param name="globalFilterExpression"></param>
    /// <param name="specificFilterExpression"></param>
    /// <returns></returns>
    Task<MethodResult<IEnumerable<T>>> SaveItemsAsync(IEnumerable<T> expectedItems, Expression<Func<T, bool>> globalFilterExpression);
}

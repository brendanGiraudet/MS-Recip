using Microsoft.EntityFrameworkCore;
using ms_recip.Data;
using ms_recip.EqualityComparer;
using ms_recip.Models;
using System.Linq.Expressions;

namespace ms_recip.Repositories.SaveBaseRepository;

public class SaveBaseRepository<T>(
    DatabaseContext databaseContext,
    ILogger logger,
    DbSet<T> dbSet) : ISaveBaseRepository<T> where T : class
{
    protected readonly DatabaseContext _databaseContext = databaseContext;
    protected readonly ILogger _logger = logger;
    protected readonly DbSet<T> _dbSet = dbSet;

    /// <inheritdoc/>
    public async Task<MethodResult<IEnumerable<T>>> SaveItemsAsync(IEnumerable<T> expectedItems, Expression<Func<T, bool>> globalFilterExpression)
    {
        try
        {
            var actualItems = _dbSet.Where(globalFilterExpression).ToList();

            foreach (var expectedItem in expectedItems)
            {
                var needToAddItem = !actualItems.Contains(expectedItem, EqualityComparerFactory<T>.CreateEqualityComparer());

                if (needToAddItem)
                {
                    await _databaseContext.AddAsync(expectedItem);
                }
            }

            foreach (var actualItem in actualItems)
            {
                var needToDeleteItem = !expectedItems.Contains(actualItem, EqualityComparerFactory<T>.CreateEqualityComparer());

                if (needToDeleteItem)
                    _databaseContext.Remove(actualItem);
            }

            await _databaseContext.SaveChangesAsync();

            return MethodResult<IEnumerable<T>>.CreateSuccessResult(expectedItems);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            return MethodResult<IEnumerable<T>>.CreateErrorResult(ex.Message);
        }
    }
}

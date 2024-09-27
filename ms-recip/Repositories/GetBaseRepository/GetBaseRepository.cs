using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ms_recip.Models;

namespace ms_recip.Repositories.GetBaseRepository;

public class GetBaseRepository<T>(ILogger logger, DbSet<T> dbSet) : IGetBaseRepository<T> where T : class
{
    protected readonly ILogger _logger = logger;
    protected readonly DbSet<T> _dbSet = dbSet;
    
    /// <inheritdoc/>
    public MethodResult<IQueryable<T>> GetItems()
    {
        try
        {
            var items = _dbSet.AsQueryable();

            return MethodResult<IQueryable<T>>.CreateSuccessResult(items);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            return MethodResult<IQueryable<T>>.CreateErrorResult(ex.Message);
        }
    }

    /// <inheritdoc/>
    public async Task<MethodResult<T>> GetItemAsync(Expression<Func<T,bool>> filterExpression)
    {
        try
        {
            var item = await _dbSet.FirstOrDefaultAsync(filterExpression);

            return MethodResult<T>.CreateSuccessResult(item);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            return MethodResult<T>.CreateErrorResult(ex.Message);
        }
    }
}

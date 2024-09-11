using Microsoft.EntityFrameworkCore;
using ms_recip.Data;
using ms_recip.Models;
using System.Linq.Expressions;

namespace ms_recip.Repositories.BaseRepository;

public class BaseRepository<T>(
    DatabaseContext databaseContext,
    ILogger logger,
    DbSet<T> dbSet) 
    : IBaseRepository<T> where T : class
{
    protected readonly DatabaseContext _databaseContext = databaseContext;
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

    /// <inheritdoc/>
    public async Task<MethodResult<T>> CreateItemAsync(T model)
    {
        try
        {
            _dbSet.Add(model);

            await _databaseContext.SaveChangesAsync();

            return MethodResult<T>.CreateSuccessResult(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            return MethodResult<T>.CreateErrorResult(ex.Message);
        }
    }

    public async Task<MethodResult<T>> UpdateItemAsync(Expression<Func<T, bool>> filterExpression, T model)
    {
        try
        {
            var actualIngredient = _dbSet.AsNoTracking().FirstOrDefault(filterExpression);

            if (actualIngredient == null) return MethodResult<T>.CreateSuccessResult(model);

            return await UpdateItemAsync(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            return MethodResult<T>.CreateErrorResult(ex.Message);
        }
    }
    
    public async Task<MethodResult<T>> UpdateItemAsync(T model)
    {
        try
        {
            _dbSet.Update(model);

            await _databaseContext.SaveChangesAsync();

            return MethodResult<T>.CreateSuccessResult(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            return MethodResult<T>.CreateErrorResult(ex.Message);
        }
    }

    public async Task<MethodResult<bool>> DeleteItemAsync(Expression<Func<T, bool>> filterExpression)
    {
        try
        {
            var actualIngredient = _dbSet.FirstOrDefault(filterExpression);

            if (actualIngredient == null) return MethodResult<bool>.CreateSuccessResult(false);

           return await DeleteItemAsync(actualIngredient);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);

            return MethodResult<bool>.CreateErrorResult(ex.Message);
        }
    }
    
    public async Task<MethodResult<bool>> DeleteItemAsync(T model)
    {
        try
        {
            _dbSet.Remove(model);

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

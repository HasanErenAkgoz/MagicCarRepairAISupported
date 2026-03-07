using MagicCarRepairAISupported.Domain.Common;
using MagicCarRepairAISupported.Domain.Repositories.EntityFrameworkCore;
using MagicCarRepairAISupported.Domain.UnitOfWork;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace MagicCarRepairAISupported.Persistence.Repositories.EntitiyFrameworkCore
{
    public class EfEntityRepository<TEntity, TContext> : IEntityRepository<TEntity>, IEntityRepository<TEntity, int> 
        where TEntity : class
        where TContext : DbContext
    {
        protected TContext Context { get; }
        private readonly IUnitOfWork _unitOfWork;

        public EfEntityRepository(TContext context, IUnitOfWork unitOfWork)
        {
            Context = context;
            _unitOfWork = unitOfWork;
        }
        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await Context.AddAsync(entity, cancellationToken);
            await _unitOfWork.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> BulkAddAsync(List<TEntity> entities)
        {
            if (entities == null || !entities.Any())
                return entities ?? new List<TEntity>();

            try
            {
                // Try using EFCore.BulkExtensions for better performance
                var bulkConfig = new BulkConfig
                {
                    SetOutputIdentity = true,
                    PreserveInsertOrder = true
                };
                
                await Context.BulkInsertAsync(entities, bulkConfig);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("DbServer") || ex.Message.Contains("Failed to create"))
            {
                // Fallback to standard EF Core AddRange if BulkExtensions fails
                await Context.AddRangeAsync(entities);
                await _unitOfWork.SaveChangesAsync();
            }
            
            return entities;
        }

        public TEntity Update(TEntity entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
            return entity;
        }

        public async Task<List<TEntity>> BulkUpdateAsync(List<TEntity> entities)
        {
            await Context.BulkUpdateAsync(entities);
            await _unitOfWork.SaveChangesAsync();
            return entities;
        }

        public TEntity Delete(TEntity entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
            return entity;

        }

        public async Task<List<TEntity>> BulkDeleteAsync(List<TEntity> entities)
        {
            await Context.BulkDeleteAsync(entities);
            await _unitOfWork.SaveChangesAsync();
            return entities;
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(CancellationToken cancellationToken = default, Expression<Func<TEntity, bool>>? expression = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (expression != null)
                query = query.Where(expression);

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(expression, cancellationToken);
        }
        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default, Expression<Func<TEntity, bool>> expression = null)
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();

            if (expression != null)
                query = query.Where(expression);

            return await query.CountAsync(cancellationToken);
        }

        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public bool Any(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().Any(expression);
        }

        public IQueryable<TEntity> Query()
        {
            return Context.Set<TEntity>();
        }

        // IEntityRepository<TEntity, TId> implementation
        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            // Try to find entity by Id property using reflection
            var entityType = typeof(TEntity);
            var idProperty = entityType.GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
            
            if (idProperty != null)
            {
                // Use FindAsync if available (works for entities with Id property)
                return await Context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
            }
            
            // Fallback: query by Id property
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, "Id");
            var constant = Expression.Constant(id);
            var equal = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, parameter);
            
            return await Context.Set<TEntity>().FirstOrDefaultAsync(lambda, cancellationToken);
        }
    }
}

using MagicCarRepairAISupported.Domain.Interfaces;
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
                await Context.AddRangeAsync(entities);
                await _unitOfWork.SaveChangesAsync();
            }
            
            return entities;
        }

        public TEntity Update(TEntity entity)
        {
            ApplyUpdate(entity);

            try
            {
                Context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                ReloadAndApplyValues(entity);
                Context.SaveChanges();
            }

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

        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var idProperty = typeof(TEntity).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance);
            
            if (idProperty != null)
            {
                return await Context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken);
            }
            
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var property = Expression.Property(parameter, "Id");
            var constant = Expression.Constant(id);
            var equal = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equal, parameter);
            
            return await Context.Set<TEntity>().FirstOrDefaultAsync(lambda, cancellationToken);
        }

        private void ApplyUpdate(TEntity entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State != EntityState.Detached)
                return;

            if (entity is IClientEntity)
            {
                ReloadAndApplyValues(entity);
                return;
            }

            Context.Update(entity);
        }

        private void ReloadAndApplyValues(TEntity entity)
        {
            var id = GetEntityId(entity);
            var existing = Context.Set<TEntity>()
                .IgnoreQueryFilters()
                .FirstOrDefault(e => EF.Property<int>(e, "Id") == id);

            if (existing == null)
                throw new DbUpdateConcurrencyException(
                    $"Cannot update {typeof(TEntity).Name} id={id}: row not found (tenant filter or deleted).");

            var concurrencyStamp = typeof(TEntity).GetProperty("ConcurrencyStamp");
            var stampBefore = concurrencyStamp?.GetValue(existing);

            Context.Entry(existing).CurrentValues.SetValues(entity);

            if (concurrencyStamp != null && stampBefore != null)
                concurrencyStamp.SetValue(existing, stampBefore);
        }

        private static int GetEntityId(TEntity entity)
        {
            var idProperty = typeof(TEntity).GetProperty("Id", BindingFlags.Public | BindingFlags.Instance)
                ?? throw new InvalidOperationException($"{typeof(TEntity).Name} has no Id property.");
            return (int)idProperty.GetValue(entity)!;
        }
    }
}

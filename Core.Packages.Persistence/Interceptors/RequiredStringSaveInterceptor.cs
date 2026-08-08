using MagicCarRepairAISupported.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MagicCarRepairAISupported.Persistence.Interceptors
{
    /// <summary>
    /// Last-resort guard: fills null/whitespace values for required string columns on save.
    /// </summary>
    public sealed class RequiredStringSaveInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            Apply(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
        {
            Apply(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private static void Apply(DbContext? context)
        {
            if (context is null)
                return;

            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.State is not (EntityState.Added or EntityState.Modified))
                    continue;

                foreach (var property in entry.Properties)
                {
                    if (property.Metadata.ClrType != typeof(string))
                        continue;

                    if (property.Metadata.IsNullable)
                        continue;

                    if (property.CurrentValue is string current && !string.IsNullOrWhiteSpace(current))
                        continue;

                    var maxLength = property.Metadata.GetMaxLength();
                    property.CurrentValue = RequiredStringDefaults.ResolveForProperty(
                        property.Metadata.Name,
                        property.CurrentValue as string,
                        maxLength);
                }
            }
        }
    }
}

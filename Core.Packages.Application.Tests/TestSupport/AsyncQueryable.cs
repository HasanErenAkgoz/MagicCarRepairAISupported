using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace MagicCarRepairAISupported.Application.Tests.TestSupport;

public static class AsyncQueryable
{
    public static IQueryable<T> AsAsyncQueryable<T>(this IEnumerable<T> source) => new TestAsyncEnumerable<T>(source);
    public static IQueryable<T> AsAsyncQueryable<T>(this IQueryable<T> source) => new TestAsyncEnumerable<T>(source);
}

internal sealed class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
{
    public TestAsyncEnumerable(IEnumerable<T> enumerable) : base(enumerable) { }
    public TestAsyncEnumerable(Expression expression) : base(expression) { }
    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default) => new TestAsyncEnumerator<T>(((IEnumerable<T>)this).GetEnumerator());
    IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<T>(this);
}
internal sealed class TestAsyncEnumerator<T>(IEnumerator<T> inner) : IAsyncEnumerator<T>
{
    public T Current => inner.Current; public ValueTask DisposeAsync() { inner.Dispose(); return ValueTask.CompletedTask; }
    public ValueTask<bool> MoveNextAsync() => ValueTask.FromResult(inner.MoveNext());
}
internal sealed class TestAsyncQueryProvider<T>(IQueryProvider inner) : IAsyncQueryProvider
{
    public IQueryable CreateQuery(Expression expression) => new TestAsyncEnumerable<T>(expression);
    public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => new TestAsyncEnumerable<TElement>(expression);
    public object? Execute(Expression expression) => inner.Execute(expression);
    public TResult Execute<TResult>(Expression expression) => inner.Execute<TResult>(expression);
    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = default)
    {
        var resultType = typeof(TResult).GetGenericArguments().Single();
        var result = typeof(IQueryProvider).GetMethods()
            .Single(m => m.Name == nameof(IQueryProvider.Execute) && m.IsGenericMethod && m.GetParameters().Length == 1)
            .MakeGenericMethod(resultType)
            .Invoke(inner, [expression]);
        return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))!
            .MakeGenericMethod(resultType).Invoke(null, [result])!;
    }
}

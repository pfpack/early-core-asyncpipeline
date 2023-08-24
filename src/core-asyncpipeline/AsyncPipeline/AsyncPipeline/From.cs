using System.Threading;
using System.Threading.Tasks;

namespace System;

partial class AsyncPipeline
{
    public static AsyncPipeline<T> From<T>(ValueTask<T> valueTask, CancellationToken cancellationToken = default)
        =>
        new(valueTask, cancellationToken, null);

    public static AsyncPipeline<T> From<T>(Task<T> task, CancellationToken cancellationToken = default)
    {
        _ = task ?? throw new ArgumentNullException(nameof(task));

        return new(valueTask: new(task), cancellationToken, null);
    }
}
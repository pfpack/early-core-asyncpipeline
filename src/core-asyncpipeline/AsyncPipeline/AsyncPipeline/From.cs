using System.Threading;
using System.Threading.Tasks;

namespace System;

partial class AsyncPipeline
{
    public static AsyncPipeline<T> From<T>(ValueTask<T> task, CancellationToken cancellationToken = default)
        =>
        new(task, cancellationToken);

    public static AsyncPipeline<T> From<T>(Task<T> task, CancellationToken cancellationToken = default)
    {
        _ = task ?? throw new ArgumentNullException(nameof(task));

        return new(task: new(task), cancellationToken);
    }
}
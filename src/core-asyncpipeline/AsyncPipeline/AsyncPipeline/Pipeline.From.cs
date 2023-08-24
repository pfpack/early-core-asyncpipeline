using System.Threading;
using System.Threading.Tasks;

namespace System;

partial class AsyncPipeline
{
    public static AsyncPipeline<T> From<T>(ValueTask<T> valueTask, CancellationToken cancellationToken = default)
        =>
        new(valueTask, null, cancellationToken);

    public static AsyncPipeline<T> From<T>(Task<T> task, CancellationToken cancellationToken = default)
    {
        _ = task ?? throw new ArgumentNullException(nameof(task));

        return new(valueTask: new(task), null, cancellationToken);
    }
}
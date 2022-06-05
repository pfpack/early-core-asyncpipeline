using System.Threading;
using System.Threading.Tasks;

namespace System;

partial class AsyncPipeline
{
    public static AsyncPipeline<T> From<T>(Task<T> task, CancellationToken cancellationToken = default)
        =>
        new(
            task ?? throw new ArgumentNullException(nameof(task)),
            cancellationToken);

    public static AsyncPipeline<T> From<T>(ValueTask<T> valueTask, CancellationToken cancellationToken = default)
        =>
        new(
            valueTask.AsTask(),
            cancellationToken);
}
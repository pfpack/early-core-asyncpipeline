using System.Threading;
using System.Threading.Tasks;

namespace System;

partial class AsyncPipeline
{
    public static AsyncPipeline<TSuccess, TFailure> From<TSuccess, TFailure>(
        ValueTask<Result<TSuccess, TFailure>> valueTask,
        CancellationToken cancellationToken = default)
        where TFailure : struct
        =>
        new(
            pipeline: new(valueTask, cancellationToken));

    public static AsyncPipeline<TSuccess, TFailure> From<TSuccess, TFailure>(
        Task<Result<TSuccess, TFailure>> task,
        CancellationToken cancellationToken = default)
        where TFailure : struct
    {
        _ = task ?? throw new ArgumentNullException(nameof(task));

        return new(
            pipeline: new(valueTask: new(task), cancellationToken));
    }
}
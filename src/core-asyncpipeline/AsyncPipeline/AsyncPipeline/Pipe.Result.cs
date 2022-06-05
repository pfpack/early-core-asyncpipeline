using System.Threading;
using System.Threading.Tasks;

namespace System;

partial class AsyncPipeline
{
    public static AsyncPipeline<TSuccess, TFailure> Pipe<TSuccess, TFailure>(
        Result<TSuccess, TFailure> value,
        CancellationToken cancellationToken = default)
        where TFailure : struct
        =>
        new(
            pipeline: new(Task.FromResult(value), cancellationToken));
}
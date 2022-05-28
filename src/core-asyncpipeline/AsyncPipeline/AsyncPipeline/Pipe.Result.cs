using System.Threading;
using System.Threading.Tasks;

namespace System;

partial class AsyncPipeline
{
    public static AsyncResultFlow<TSuccess, TFailure> Pipe<TSuccess, TFailure>(
        Result<TSuccess, TFailure> value,
        CancellationToken cancellationToken = default)
        where TFailure : struct
        =>
        new(
            asyncPipeline: new(ValueTask.FromResult(value), cancellationToken));
}
using System.Threading;

namespace System;

partial struct ConfigurableAsyncPipeline
{
    public AsyncPipeline<TSuccess, TFailure> Pipe<TSuccess, TFailure>(
        Result<TSuccess, TFailure> value,
        CancellationToken cancellationToken = default)
        where TFailure : struct
        =>
        new(
            pipeline: new(valueTask: new(value), configuration, cancellationToken));
}
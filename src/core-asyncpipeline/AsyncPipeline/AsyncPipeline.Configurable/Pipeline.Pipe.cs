using System.Threading;

namespace System;

partial struct ConfigurableAsyncPipeline
{
    public AsyncPipeline<T> Pipe<T>(T value, CancellationToken cancellationToken = default)
        =>
        new(valueTask: new(value), configuration, cancellationToken);
}
using System.Threading;

namespace System;

partial class AsyncPipeline
{
    public static AsyncPipeline<T> Pipe<T>(T value, CancellationToken cancellationToken = default)
        =>
        new(valueTask: new(value), cancellationToken, null);
}
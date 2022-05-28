using System.Threading;

namespace System;

partial class AsyncPipeline
{
    public static AsyncPipeline<T> Pipe<T>(T value, CancellationToken cancellationToken = default)
        =>
        InnerPipe(value, cancellationToken);
}
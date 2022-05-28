using System.Threading;

namespace System;

partial class AsyncPipeline
{
    private static AsyncPipeline<T> InnerPipe<T>(T value, CancellationToken cancellationToken)
        =>
        new(valueTask: new(value), isCanceled: false, cancellationToken: cancellationToken);
}
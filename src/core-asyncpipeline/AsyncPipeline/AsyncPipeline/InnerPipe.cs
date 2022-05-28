using System.Threading;
using System.Threading.Tasks;

namespace System;

partial class AsyncPipeline
{
    private static AsyncPipeline<T> InnerPipe<T>(T value, CancellationToken cancellationToken)
        =>
        new(ValueTask.FromResult(value), cancellationToken);
}
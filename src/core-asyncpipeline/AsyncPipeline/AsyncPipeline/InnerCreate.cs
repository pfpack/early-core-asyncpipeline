using System.Threading;
using System.Threading.Tasks;

namespace System;

partial class AsyncPipeline
{
    private static AsyncPipeline<T> InnerCreate<T>(T value, CancellationToken cancellationToken)
        =>
        new(ValueTask.FromResult(value), cancellationToken);
}
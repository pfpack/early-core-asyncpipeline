using System.Threading;
using System.Threading.Tasks;

namespace System;

partial class AsyncPipeline
{
    public static AsyncPipeline<T> Pipe<T>(T value, CancellationToken cancellationToken = default)
        =>
        new(ValueTask.FromResult(value), cancellationToken);
}
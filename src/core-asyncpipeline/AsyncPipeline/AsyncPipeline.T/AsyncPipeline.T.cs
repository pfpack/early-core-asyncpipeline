using System.Threading;
using System.Threading.Tasks;

namespace System;

public readonly partial struct AsyncPipeline<T> : IEquatable<AsyncPipeline<T>>
{
    private static CancellationToken CanceledToken() => new(canceled: true);

    private readonly bool isCanceled;

    private readonly ValueTask<T> valueTask;

    private readonly CancellationToken cancellationToken;

    internal AsyncPipeline(ValueTask<T> valueTask, CancellationToken cancellationToken)
    {
        isCanceled = false;
        this.valueTask = valueTask;
        this.cancellationToken = cancellationToken;
    }

    private AsyncPipeline(int _)
    {
        isCanceled = true;
        valueTask = default;
        cancellationToken = default;
    }
}
using System.Threading;
using System.Threading.Tasks;

namespace System;

public readonly partial struct AsyncPipeline<T> : IEquatable<AsyncPipeline<T>>
{
    private static CancellationToken CanceledToken() => new(canceled: true);

    private readonly ValueTask<T> valueTask;

    private readonly bool isCanceled;

    private readonly CancellationToken cancellationToken;

    internal AsyncPipeline(ValueTask<T> valueTask, CancellationToken cancellationToken)
    {
        this.valueTask = valueTask;
        isCanceled = false;
        this.cancellationToken = cancellationToken;
    }

    private AsyncPipeline(CancellationToken cancellationToken)
    {
        valueTask = default;
        isCanceled = true;
        this.cancellationToken = cancellationToken;
    }
}
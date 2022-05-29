using System.Threading;
using System.Threading.Tasks;

namespace System;

public readonly partial struct AsyncPipeline<T> : IEquatable<AsyncPipeline<T>>
{
    private static CancellationToken CanceledToken() => new(canceled: true);

    private readonly bool isCanceled;

    private readonly ValueTask<T> valueTask;

    private readonly CancellationToken cancellationToken;

    // Creates a non-canceled pipeline
    internal AsyncPipeline(ValueTask<T> valueTask, CancellationToken cancellationToken)
    {
        isCanceled = false;
        this.valueTask = valueTask;
        this.cancellationToken = cancellationToken;
    }

    // Creates the canceled pipeline
    // The unused arg is intended to separate this constructor from the default constructor
    // which creates the non-canceled default pipeline
    private AsyncPipeline(int _)
    {
        isCanceled = true;
        valueTask = default;
        cancellationToken = default;
    }
}
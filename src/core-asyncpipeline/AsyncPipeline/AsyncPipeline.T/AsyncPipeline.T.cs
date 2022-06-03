using System.Threading;
using System.Threading.Tasks;

namespace System;

public readonly partial struct AsyncPipeline<T> : IEquatable<AsyncPipeline<T>>
{
    private static CancellationToken CanceledToken() => new(canceled: true);

    private readonly bool isStopped;

    private readonly ValueTask<T> valueTask;

    private readonly CancellationToken cancellationToken;

    // Creates a non-stopped pipeline
    internal AsyncPipeline(ValueTask<T> valueTask, CancellationToken cancellationToken)
    {
        isStopped = false;
        this.valueTask = valueTask;
        this.cancellationToken = cancellationToken;
    }

    // Creates the stopped pipeline
    // The unused arg is intended to separate this constructor from the default constructor
    // which creates the non-stopped default pipeline with the task completed state
    private AsyncPipeline(int _)
    {
        isStopped = true;
        valueTask = default;
        cancellationToken = default;
    }
}
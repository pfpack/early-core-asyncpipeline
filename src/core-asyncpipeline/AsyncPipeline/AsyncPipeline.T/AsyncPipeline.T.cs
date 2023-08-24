using System.Threading;
using System.Threading.Tasks;

namespace System;

public readonly partial struct AsyncPipeline<T> : IEquatable<AsyncPipeline<T>>
{
    private static CancellationToken CanceledToken() => new(canceled: true);

    private readonly bool isStopped;

    private readonly ValueTask<T> valueTask;

    private readonly AsyncPipelineOptions? options;

    private readonly CancellationToken cancellationToken;

    // Creates a non-stopped pipeline
    internal AsyncPipeline(ValueTask<T> valueTask, AsyncPipelineOptions? options, CancellationToken cancellationToken)
    {
        isStopped = false;
        this.valueTask = valueTask;
        this.options = options;
        this.cancellationToken = cancellationToken;
    }

    // Creates the stopped pipeline
    // The unused arg is intended to separate this constructor from the default constructor
    // which creates the non-stopped pipeline with the task completed state and default result
    private AsyncPipeline(int _)
    {
        isStopped = true;
        valueTask = default;
        cancellationToken = default;
        options = null;
    }

    public AsyncPipelineOptions Options
        =>
        options ?? InnerEmptyOptions.Value;

    private static class InnerEmptyOptions
    {
        internal static readonly AsyncPipelineOptions Value = new();
    }
}
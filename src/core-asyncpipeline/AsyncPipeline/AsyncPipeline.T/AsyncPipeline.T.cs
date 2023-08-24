using System.Threading;
using System.Threading.Tasks;

namespace System;

public readonly partial struct AsyncPipeline<T> : IEquatable<AsyncPipeline<T>>
{
    private static CancellationToken CanceledToken() => new(canceled: true);

    private readonly bool isStopped;

    private readonly ValueTask<T> valueTask;

    private readonly CancellationToken cancellationToken;

    private readonly AsyncPipelineOptions? options;

    // Creates a non-stopped pipeline
    internal AsyncPipeline(ValueTask<T> valueTask, CancellationToken cancellationToken, AsyncPipelineOptions? options)
    {
        isStopped = false;
        this.valueTask = valueTask;
        this.cancellationToken = cancellationToken;
        this.options = options;
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
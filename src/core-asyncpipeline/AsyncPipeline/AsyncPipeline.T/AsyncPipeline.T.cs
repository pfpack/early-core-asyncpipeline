using System.Threading;
using System.Threading.Tasks;

namespace System;

public readonly partial struct AsyncPipeline<T> : IEquatable<AsyncPipeline<T>>
{
    private static CancellationToken CanceledToken() => new(canceled: true);

    private readonly bool isStopped;

    private readonly ValueTask<T> valueTask;

    private readonly AsyncPipelineConfiguration? configuration;

    private readonly CancellationToken cancellationToken;

    // Creates a non-stopped pipeline
    internal AsyncPipeline(ValueTask<T> valueTask, AsyncPipelineConfiguration? configuration, CancellationToken cancellationToken)
    {
        isStopped = false;
        this.valueTask = valueTask;
        this.configuration = configuration;
        this.cancellationToken = cancellationToken;
    }

    // Creates the stopped pipeline
    // The unused arg is intended to separate this constructor from the default constructor
    // which creates the non-stopped pipeline with the task completed state and default result
    private AsyncPipeline(int _)
    {
        isStopped = true;
        valueTask = default;
        configuration = null;
        cancellationToken = default;
    }

    public AsyncPipelineConfiguration Configuration
        =>
        configuration ?? InnerDefaultConfiguration.Value;

    private static class InnerDefaultConfiguration
    {
        internal static readonly AsyncPipelineConfiguration Value = new();
    }
}
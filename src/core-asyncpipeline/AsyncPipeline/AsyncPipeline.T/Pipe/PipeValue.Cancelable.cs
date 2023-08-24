using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public AsyncPipeline<TResult> PipeValue<TResult>(Func<T, CancellationToken, ValueTask<TResult>> pipeAsync)
        =>
        InternalPipeValue(
            pipeAsync ?? throw new ArgumentNullException(nameof(pipeAsync)));

    internal AsyncPipeline<TResult> InternalPipeValue<TResult>(Func<T, CancellationToken, ValueTask<TResult>> pipeAsync)
        =>
        isStopped is false
            ? new(InnerInvokeValueAsync(pipeAsync), options, cancellationToken)
            : new(default);

    private async ValueTask<TResult> InnerInvokeValueAsync<TResult>(Func<T, CancellationToken, ValueTask<TResult>> pipeAsync)
    {
        var result = await valueTask.ConfigureAwait(Options.ContinueOnCapturedContext);
        return await pipeAsync.Invoke(result, cancellationToken).ConfigureAwait(Options.ContinueOnCapturedContext);
    }
}
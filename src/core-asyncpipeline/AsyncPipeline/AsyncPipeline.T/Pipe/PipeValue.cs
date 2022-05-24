using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public AsyncPipeline<TResult> PipeValue<TResult>(Func<T, ValueTask<TResult>> pipeAsync)
        =>
        InternalPipeValue(
            pipeAsync ?? throw new ArgumentNullException(nameof(pipeAsync)));

    internal AsyncPipeline<TResult> InternalPipeValue<TResult>(Func<T, ValueTask<TResult>> pipeAsync)
        =>
        isCanceled
            ? new(valueTask: default, isCanceled: true, cancellationToken: cancellationToken)
            : new(valueTask: InnerInvokeValueAsync(pipeAsync), isCanceled: false, cancellationToken: cancellationToken);

    private async ValueTask<TResult> InnerInvokeValueAsync<TResult>(Func<T, ValueTask<TResult>> pipeAsync)
    {
        var result = await valueTask.ConfigureAwait(false);
        return await pipeAsync.Invoke(result).ConfigureAwait(false);
    }
}
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public AsyncPipeline<TResult> Pipe<TResult>(Func<T, Task<TResult>> pipeAsync)
        =>
        InternalPipe(
            pipeAsync ?? throw new ArgumentNullException(nameof(pipeAsync)));

    internal AsyncPipeline<TResult> InternalPipe<TResult>(Func<T, Task<TResult>> pipeAsync)
        =>
        isStopped is false
            ? new(InnerInvokeAsync(pipeAsync), configuration, cancellationToken)
            : new(default);

    private async ValueTask<TResult> InnerInvokeAsync<TResult>(Func<T, Task<TResult>> pipeAsync)
    {
        var result = await valueTask.ConfigureAwait(Configuration.ContinueOnCapturedContext);
        return await pipeAsync.Invoke(result).ConfigureAwait(Configuration.ContinueOnCapturedContext);
    }
}
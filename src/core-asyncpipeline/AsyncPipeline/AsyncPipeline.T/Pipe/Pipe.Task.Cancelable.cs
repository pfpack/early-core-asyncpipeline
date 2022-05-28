using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public AsyncPipeline<TResult> Pipe<TResult>(Func<T, CancellationToken, Task<TResult>> pipeAsync)
        =>
        InternalPipe(
            pipeAsync ?? throw new ArgumentNullException(nameof(pipeAsync)));

    internal AsyncPipeline<TResult> InternalPipe<TResult>(Func<T, CancellationToken, Task<TResult>> pipeAsync)
        =>
        isCanceled
            ? new(default)
            : new(InnerInvokeAsync(pipeAsync), cancellationToken);

    private async ValueTask<TResult> InnerInvokeAsync<TResult>(Func<T, CancellationToken, Task<TResult>> pipeAsync)
    {
        var result = await valueTask.ConfigureAwait(false);
        return await pipeAsync.Invoke(result, cancellationToken).ConfigureAwait(false);
    }
}
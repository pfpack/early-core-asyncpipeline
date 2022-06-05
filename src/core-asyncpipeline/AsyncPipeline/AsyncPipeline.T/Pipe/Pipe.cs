using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public AsyncPipeline<TResult> Pipe<TResult>(Func<T, TResult> pipe)
        =>
        InternalPipe(
            pipe ?? throw new ArgumentNullException(nameof(pipe)));

    internal AsyncPipeline<TResult> InternalPipe<TResult>(Func<T, TResult> pipe)
        =>
        isStopped is false
            ? new(InnerInvokeAsync(pipe), cancellationToken)
            : new(default);

    private async Task<TResult> InnerInvokeAsync<TResult>(Func<T, TResult> pipe)
    {
        var result = await GetTask().ConfigureAwait(false);
        return pipe.Invoke(result);
    }
}
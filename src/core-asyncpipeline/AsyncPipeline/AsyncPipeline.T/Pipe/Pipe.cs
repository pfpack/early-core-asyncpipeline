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
            ? new(InnerInvokeAsync(pipe), cancellationToken, options)
            : new(default);

    private async ValueTask<TResult> InnerInvokeAsync<TResult>(Func<T, TResult> pipe)
    {
        var result = await valueTask.ConfigureAwait(false);
        return pipe.Invoke(result);
    }
}
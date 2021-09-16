#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public AsyncPipeline<TResult> Pipe<TResult>(Func<T, CancellationToken, Task<TResult>> pipeAsync)
            =>
            InternalPipe(
                pipeAsync ?? throw new ArgumentNullException(nameof(pipeAsync)));

        internal AsyncPipeline<TResult> InternalPipe<TResult>(Func<T, CancellationToken, Task<TResult>> pipeAsync)
            =>
            isCanceled || cancellationToken.IsCancellationRequested
                ? new(valueTask: default, isCanceled: true, cancellationToken: cancellationToken)
                : new(valueTask: InnerPipeAsync(pipeAsync), isCanceled: false, cancellationToken: cancellationToken);

        private async ValueTask<TResult> InnerPipeAsync<TResult>(Func<T, CancellationToken, Task<TResult>> pipeAsync)
        {
            var result = await valueTask.ConfigureAwait(false);
            return await pipeAsync.Invoke(result, cancellationToken).ConfigureAwait(false);
        }
    }
}
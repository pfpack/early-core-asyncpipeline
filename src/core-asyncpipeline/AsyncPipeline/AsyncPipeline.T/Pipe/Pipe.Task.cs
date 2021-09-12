#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public AsyncPipeline<TResult> Pipe<TResult>(Func<T, CancellationToken, Task<TResult>> mapAsync)
            =>
            InternalPipe(
                mapAsync ?? throw new ArgumentNullException(nameof(mapAsync)));

        internal AsyncPipeline<TResult> InternalPipe<TResult>(Func<T, CancellationToken, Task<TResult>> mapAsync)
            =>
            hasCanceled || cancellationToken.IsCancellationRequested
                ? new(valueTask: default, hasCanceled: true, cancellationToken: cancellationToken)
                : new(valueTask: InnerPipeAsync(mapAsync), hasCanceled: hasCanceled, cancellationToken: cancellationToken);

        private async ValueTask<TResult> InnerPipeAsync<TResult>(Func<T, CancellationToken, Task<TResult>> mapAsync)
        {
            var result = await valueTask.ConfigureAwait(false);
            return await mapAsync.Invoke(result, cancellationToken).ConfigureAwait(false);
        }
    }
}
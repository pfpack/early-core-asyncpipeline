#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public AsyncPipeline<TResult> PipeValue<TResult>(
            Func<T, CancellationToken, ValueTask<TResult>> pipeAsync)
            =>
            InternalPipeValue(
                pipeAsync ?? throw new ArgumentNullException(nameof(pipeAsync)));

        internal AsyncPipeline<TResult> InternalPipeValue<TResult>(Func<T, CancellationToken, ValueTask<TResult>> pipeAsync)
            =>
            isCanceled || cancellationToken.IsCancellationRequested
                ? new(valueTask: default, isCanceled: true, cancellationToken: cancellationToken)
                : new(valueTask: InnerPipeValueAsync(pipeAsync), isCanceled: false, cancellationToken: cancellationToken);

        private async ValueTask<TResult> InnerPipeValueAsync<TResult>(Func<T, CancellationToken, ValueTask<TResult>> pipeAsync)
        {
            var result = await valueTask.ConfigureAwait(false);
            return await pipeAsync.Invoke(result, cancellationToken).ConfigureAwait(false);
        }
    }
}
#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public AsyncPipeline<TResult> PipeValue<TResult>(
            Func<T, CancellationToken, ValueTask<TResult>> mapAsync)
            =>
            InternalPipeValue(
                mapAsync ?? throw new ArgumentNullException(nameof(mapAsync)));

        internal AsyncPipeline<TResult> InternalPipeValue<TResult>(Func<T, CancellationToken, ValueTask<TResult>> mapAsync)
            =>
            isCanceled || cancellationToken.IsCancellationRequested
                ? new(valueTask: default, isCanceled: true, cancellationToken: cancellationToken)
                : new(valueTask: InnerPipeValueAsync(mapAsync), isCanceled: false, cancellationToken: cancellationToken);

        private async ValueTask<TResult> InnerPipeValueAsync<TResult>(Func<T, CancellationToken, ValueTask<TResult>> mapAsync)
        {
            var result = await valueTask.ConfigureAwait(false);
            return await mapAsync.Invoke(result, cancellationToken).ConfigureAwait(false);
        }
    }
}
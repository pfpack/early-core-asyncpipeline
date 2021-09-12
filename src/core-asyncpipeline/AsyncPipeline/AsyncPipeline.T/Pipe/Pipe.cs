#nullable enable

using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public AsyncPipeline<TResult> Pipe<TResult>(Func<T, TResult> map)
            =>
            InternalPipe(
                map ?? throw new ArgumentNullException(nameof(map)));

        internal AsyncPipeline<TResult> InternalPipe<TResult>(Func<T, TResult> map)
            =>
            hasCanceled
                ? new(valueTask: default, hasCanceled: true, cancellationToken: cancellationToken)
                : new(valueTask: InnerPipeAsync(map), hasCanceled: hasCanceled, cancellationToken: cancellationToken);

        private async ValueTask<TResult> InnerPipeAsync<TResult>(Func<T, TResult> map)
        {
            var result = await valueTask.ConfigureAwait(false);
            return map.Invoke(result);
        }
    }
}
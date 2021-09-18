#nullable enable

using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public AsyncPipeline<TResult> Pipe<TResult>(Func<T, TResult> pipe)
            =>
            InternalPipe(
                pipe ?? throw new ArgumentNullException(nameof(pipe)));

        internal AsyncPipeline<TResult> InternalPipe<TResult>(Func<T, TResult> pipe)
            =>
            isCanceled
                ? new(valueTask: default, isCanceled: true, cancellationToken: cancellationToken)
                : new(valueTask: InnerPipeAsync(pipe), isCanceled: false, cancellationToken: cancellationToken);

        private async ValueTask<TResult> InnerPipeAsync<TResult>(Func<T, TResult> pipe)
        {
            var result = await valueTask.ConfigureAwait(false);
            return pipe.Invoke(result);
        }
    }
}
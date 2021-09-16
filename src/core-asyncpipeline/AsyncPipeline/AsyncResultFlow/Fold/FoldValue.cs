#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncPipeline<TResult> FoldValue<TResult>(
            Func<TSuccess, CancellationToken, ValueTask<TResult>> mapSuccessAsync,
            Func<TFailure, TResult> mapFailure)
            =>
            InnerFilterValue(
                mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)),
                mapFailure ?? throw new ArgumentNullException(nameof(mapFailure)));

        public AsyncPipeline<TResult> FoldValue<TResult>(
            Func<TSuccess, CancellationToken, ValueTask<TResult>> mapSuccessAsync,
            Func<TFailure, CancellationToken, ValueTask<TResult>> mapFailureAsync)
            =>
            InnerFilterValue(
                mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)),
                mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

        private AsyncPipeline<TResult> InnerFilterValue<TResult>(
            Func<TSuccess, CancellationToken, ValueTask<TResult>> mapSuccessAsync,
            Func<TFailure, TResult> mapFailure)
            =>
            asyncPipeline.InternalPipeValue(
                (r, t) => r.FoldValueAsync(
                    s => mapSuccessAsync.Invoke(s, t),
                    f => f.InternalPipe(mapFailure).InternalPipe(ValueTask.FromResult)));

        private AsyncPipeline<TResult> InnerFilterValue<TResult>(
            Func<TSuccess, CancellationToken, ValueTask<TResult>> mapSuccessAsync,
            Func<TFailure, CancellationToken, ValueTask<TResult>> mapFailureAsync)
            =>
            asyncPipeline.InternalPipeValue(
                (r, t) => r.FoldValueAsync(
                    s => mapSuccessAsync.Invoke(s, t),
                    f => mapFailureAsync.Invoke(f, t)));
    }
}
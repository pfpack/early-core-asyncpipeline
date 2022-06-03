using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<TSuccess, TFailure>
    {
        public AsyncPipeline<TResult> FoldValue<TResult>(
            Func<TSuccess, CancellationToken, ValueTask<TResult>> mapSuccessAsync,
            Func<TFailure, CancellationToken, ValueTask<TResult>> mapFailureAsync)
            =>
            InnerFilterValue(
                mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)),
                mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

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
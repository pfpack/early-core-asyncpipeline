using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncPipeline<TResult> Fold<TResult>(
            Func<TSuccess, CancellationToken, Task<TResult>> mapSuccessAsync,
            Func<TFailure, CancellationToken, Task<TResult>> mapFailureAsync)
            =>
            InnerFilter(
                mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)),
                mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

        private AsyncPipeline<TResult> InnerFilter<TResult>(
            Func<TSuccess, CancellationToken, Task<TResult>> mapSuccessAsync,
            Func<TFailure, CancellationToken, Task<TResult>> mapFailureAsync)
            =>
            asyncPipeline.InternalPipe(
                (r, t) => r.FoldAsync(
                    s => mapSuccessAsync.Invoke(s, t),
                    f => mapFailureAsync.Invoke(f, t)));
    }
}
using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<TSuccess, TFailure>
    {
        public AsyncPipeline<TResultSuccess, TResultFailure> Fold<TResultSuccess, TResultFailure>(
            Func<TSuccess, CancellationToken, Task<Result<TResultSuccess, TResultFailure>>> mapSuccessAsync,
            Func<TFailure, CancellationToken, Task<Result<TResultSuccess, TResultFailure>>> mapFailureAsync)
            where TResultFailure : struct
            =>
            InnerFilter(
                mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)),
                mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

        private AsyncPipeline<TResultSuccess, TResultFailure> InnerFilter<TResultSuccess, TResultFailure>(
            Func<TSuccess, CancellationToken, Task<Result<TResultSuccess, TResultFailure>>> mapSuccessAsync,
            Func<TFailure, CancellationToken, Task<Result<TResultSuccess, TResultFailure>>> mapFailureAsync)
            where TResultFailure : struct
            =>
            new(
                pipeline.InternalPipe(
                    (r, t) => r.FoldAsync(
                        s => mapSuccessAsync.Invoke(s, t),
                        f => mapFailureAsync.Invoke(f, t))));
    }
}
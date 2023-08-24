using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<TSuccess, TFailure>
    {
        public AsyncPipeline<TResultSuccess, TResultFailure> FoldValue<TResultSuccess, TResultFailure>(
            Func<TSuccess, CancellationToken, ValueTask<Result<TResultSuccess, TResultFailure>>> mapSuccessAsync,
            Func<TFailure, CancellationToken, ValueTask<Result<TResultSuccess, TResultFailure>>> mapFailureAsync)
            where TResultFailure : struct
            =>
            InnerFoldValue(
                mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)),
                mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

        private AsyncPipeline<TResultSuccess, TResultFailure> InnerFoldValue<TResultSuccess, TResultFailure>(
            Func<TSuccess, CancellationToken, ValueTask<Result<TResultSuccess, TResultFailure>>> mapSuccessAsync,
            Func<TFailure, CancellationToken, ValueTask<Result<TResultSuccess, TResultFailure>>> mapFailureAsync)
            where TResultFailure : struct
            =>
            new(
                pipeline.InternalPipeValue(
                    (r, t) => r.FoldValueAsync(
                        s => mapSuccessAsync.Invoke(s, t),
                        f => mapFailureAsync.Invoke(f, t))));
    }
}
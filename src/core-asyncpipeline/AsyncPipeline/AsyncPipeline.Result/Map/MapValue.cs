using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<TSuccess, TFailure>
    {
        public AsyncPipeline<TResultSuccess, TResultFailure> MapValue<TResultSuccess, TResultFailure>(
            Func<TSuccess, CancellationToken, ValueTask<TResultSuccess>> mapSuccessAsync,
            Func<TFailure, CancellationToken, ValueTask<TResultFailure>> mapFailureAsync)
            where TResultFailure : struct
            =>
            InnerMapValue(
                mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)),
                mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

        private AsyncPipeline<TResultSuccess, TResultFailure> InnerMapValue<TResultSuccess, TResultFailure>(
            Func<TSuccess, CancellationToken, ValueTask<TResultSuccess>> mapSuccessAsync,
            Func<TFailure, CancellationToken, ValueTask<TResultFailure>> mapFailureAsync)
            where TResultFailure : struct
            =>
            InnerPipeValue(
                (r, t) => r.MapValueAsync(
                    s => mapSuccessAsync.Invoke(s, t),
                    f => mapFailureAsync.Invoke(f, t)));
    }
}
#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncResultFlow<TSuccess, TResultFailure> MapFailureValue<TResultFailure>(
            Func<TFailure, CancellationToken, ValueTask<TResultFailure>> mapFailureAsync)
            where TResultFailure : struct
            =>
            InnerMapFailureValue(
                mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

        private AsyncResultFlow<TSuccess, TResultFailure> InnerMapFailureValue<TResultFailure>(
            Func<TFailure, CancellationToken, ValueTask<TResultFailure>> mapFailureAsync)
            where TResultFailure : struct
            =>
            InnerPipeValue(
                (r, t) => r.MapFailureValueAsync(
                    s => mapFailureAsync.Invoke(s, t)));
    }
}
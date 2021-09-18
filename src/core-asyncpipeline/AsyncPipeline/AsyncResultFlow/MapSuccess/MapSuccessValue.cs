#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncResultFlow<TResultSuccess, TFailure> MapSuccessValue<TResultSuccess>(
            Func<TSuccess, CancellationToken, ValueTask<TResultSuccess>> mapSuccessAsync)
            =>
            InnerMapSuccessValue(
                mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)));

        private AsyncResultFlow<TResultSuccess, TFailure> InnerMapSuccessValue<TResultSuccess>(
            Func<TSuccess, CancellationToken, ValueTask<TResultSuccess>> mapSuccessAsync)
            =>
            InnerPipeValue(
                (r, t) => r.MapSuccessValueAsync(
                    s => mapSuccessAsync.Invoke(s, t)));
    }
}
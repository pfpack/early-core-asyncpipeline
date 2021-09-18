#nullable enable

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncResultFlow<TResultSuccess, TFailure> MapSuccess<TResultSuccess>(Func<TSuccess, TResultSuccess> mapSuccess)
            =>
            InnerMapSuccess(
                mapSuccess ?? throw new ArgumentNullException(nameof(mapSuccess)));

        private AsyncResultFlow<TResultSuccess, TFailure> InnerMapSuccess<TResultSuccess>(Func<TSuccess, TResultSuccess> mapSuccess)
            =>
            InnerPipe(
                r => r.MapSuccess(mapSuccess));
    }
}
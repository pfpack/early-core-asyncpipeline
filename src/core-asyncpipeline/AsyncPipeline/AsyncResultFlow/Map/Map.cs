#nullable enable

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncResultFlow<TResultSuccess, TResultFailure> Map<TResultSuccess, TResultFailure>(
            Func<TSuccess, TResultSuccess> mapSuccess,
            Func<TFailure, TResultFailure> mapFailure)
            where TResultFailure : struct
            =>
            InnerMap(
                mapSuccess ?? throw new ArgumentNullException(nameof(mapSuccess)),
                mapFailure ?? throw new ArgumentNullException(nameof(mapFailure)));

        private AsyncResultFlow<TResultSuccess, TResultFailure> InnerMap<TResultSuccess, TResultFailure>(
            Func<TSuccess, TResultSuccess> mapSuccess,
            Func<TFailure, TResultFailure> mapFailure)
            where TResultFailure : struct
            =>
            InnerPipe(
                r => r.Map(mapSuccess, mapFailure));
    }
}
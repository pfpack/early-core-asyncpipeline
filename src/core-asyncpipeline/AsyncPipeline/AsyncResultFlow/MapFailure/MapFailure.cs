namespace System;

partial struct AsyncResultFlow<TSuccess, TFailure>
{
    public AsyncResultFlow<TSuccess, TResultFailure> MapFailure<TResultFailure>(Func<TFailure, TResultFailure> mapFailure)
        where TResultFailure : struct
        =>
        InnerMapFailure(
            mapFailure ?? throw new ArgumentNullException(nameof(mapFailure)));

    private AsyncResultFlow<TSuccess, TResultFailure> InnerMapFailure<TResultFailure>(Func<TFailure, TResultFailure> mapFailure)
        where TResultFailure : struct
        =>
        InnerPipe(
            result => result.MapFailure(mapFailure));
}
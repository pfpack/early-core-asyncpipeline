namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess, TResultFailure> MapFailure<TResultFailure>(Func<TFailure, TResultFailure> mapFailure)
        where TResultFailure : struct
        =>
        InnerMapFailure(
            mapFailure ?? throw new ArgumentNullException(nameof(mapFailure)));

    private AsyncPipeline<TSuccess, TResultFailure> InnerMapFailure<TResultFailure>(Func<TFailure, TResultFailure> mapFailure)
        where TResultFailure : struct
        =>
        InnerPipe(
            result => result.MapFailure(mapFailure));
}
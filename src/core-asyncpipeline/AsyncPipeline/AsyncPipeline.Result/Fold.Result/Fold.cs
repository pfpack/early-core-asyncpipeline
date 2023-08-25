namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TResultSuccess, TResultFailure> Fold<TResultSuccess, TResultFailure>(
        Func<TSuccess, Result<TResultSuccess, TResultFailure>> mapSuccess,
        Func<TFailure, Result<TResultSuccess, TResultFailure>> mapFailure)
        where TResultFailure : struct
        =>
        InnerFold(
            mapSuccess ?? throw new ArgumentNullException(nameof(mapSuccess)),
            mapFailure ?? throw new ArgumentNullException(nameof(mapFailure)));

    private AsyncPipeline<TResultSuccess, TResultFailure> InnerFold<TResultSuccess, TResultFailure>(
        Func<TSuccess, Result<TResultSuccess, TResultFailure>> mapSuccess,
        Func<TFailure, Result<TResultSuccess, TResultFailure>> mapFailure)
        where TResultFailure : struct
        =>
        new(
            pipeline.InternalPipe(
                r => r.Fold(mapSuccess, mapFailure)));
}
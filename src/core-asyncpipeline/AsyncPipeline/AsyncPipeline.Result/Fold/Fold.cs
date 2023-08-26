namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TResult> Fold<TResult>(Func<TSuccess, TResult> mapSuccess, Func<TFailure, TResult> mapFailure)
        =>
        InnerFold(
            mapSuccess ?? throw new ArgumentNullException(nameof(mapSuccess)),
            mapFailure ?? throw new ArgumentNullException(nameof(mapFailure)));

    private AsyncPipeline<TResult> InnerFold<TResult>(Func<TSuccess, TResult> mapSuccess, Func<TFailure, TResult> mapFailure)
        =>
        pipeline.InternalPipe(
            r => r.Fold(mapSuccess, mapFailure));
}
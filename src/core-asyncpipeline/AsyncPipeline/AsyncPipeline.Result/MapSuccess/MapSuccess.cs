namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TResultSuccess, TFailure> MapSuccess<TResultSuccess>(Func<TSuccess, TResultSuccess> mapSuccess)
        =>
        InnerMapSuccess(
            mapSuccess ?? throw new ArgumentNullException(nameof(mapSuccess)));

    private AsyncPipeline<TResultSuccess, TFailure> InnerMapSuccess<TResultSuccess>(Func<TSuccess, TResultSuccess> mapSuccess)
        =>
        InnerPipe(
            result => result.MapSuccess(mapSuccess));
}
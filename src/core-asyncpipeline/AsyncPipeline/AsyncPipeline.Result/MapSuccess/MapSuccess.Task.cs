using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TResultSuccess, TFailure> MapSuccess<TResultSuccess>(
        Func<TSuccess, Task<TResultSuccess>> mapSuccessAsync)
        =>
        InnerMapSuccess(
            mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)));

    private AsyncPipeline<TResultSuccess, TFailure> InnerMapSuccess<TResultSuccess>(
        Func<TSuccess, Task<TResultSuccess>> mapSuccessAsync)
        =>
        new(
            asyncPipeline.InternalPipe(
                result => result.MapSuccessAsync(mapSuccessAsync)));
}
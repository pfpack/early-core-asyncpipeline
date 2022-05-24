using System.Threading.Tasks;

namespace System;

partial struct AsyncResultFlow<TSuccess, TFailure>
{
    public AsyncResultFlow<TResultSuccess, TFailure> MapSuccess<TResultSuccess>(
        Func<TSuccess, Task<TResultSuccess>> mapSuccessAsync)
        =>
        InnerMapSuccess(
            mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)));

    private AsyncResultFlow<TResultSuccess, TFailure> InnerMapSuccess<TResultSuccess>(
        Func<TSuccess, Task<TResultSuccess>> mapSuccessAsync)
        =>
        new(
            asyncPipeline.InternalPipe(
                result => result.MapSuccessAsync(mapSuccessAsync)));
}
using System.Threading.Tasks;

namespace System;

partial struct AsyncResultFlow<TSuccess, TFailure>
{
    public AsyncResultFlow<TResultSuccess, TFailure> MapSuccessValue<TResultSuccess>(
        Func<TSuccess, ValueTask<TResultSuccess>> mapSuccessAsync)
        =>
        InnerMapSuccessValue(
            mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)));

    private AsyncResultFlow<TResultSuccess, TFailure> InnerMapSuccessValue<TResultSuccess>(
        Func<TSuccess, ValueTask<TResultSuccess>> mapSuccessAsync)
        =>
        new(
            asyncPipeline.InternalPipeValue(
                result => result.MapSuccessValueAsync(mapSuccessAsync)));
}
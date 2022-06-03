using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TResultSuccess, TFailure> MapSuccessValue<TResultSuccess>(
        Func<TSuccess, ValueTask<TResultSuccess>> mapSuccessAsync)
        =>
        InnerMapSuccessValue(
            mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)));

    private AsyncPipeline<TResultSuccess, TFailure> InnerMapSuccessValue<TResultSuccess>(
        Func<TSuccess, ValueTask<TResultSuccess>> mapSuccessAsync)
        =>
        new(
            pipeline.InternalPipeValue(
                result => result.MapSuccessValueAsync(mapSuccessAsync)));
}
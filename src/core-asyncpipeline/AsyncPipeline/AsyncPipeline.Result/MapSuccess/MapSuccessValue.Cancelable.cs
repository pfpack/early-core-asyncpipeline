using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TResultSuccess, TFailure> MapSuccessValue<TResultSuccess>(
        Func<TSuccess, CancellationToken, ValueTask<TResultSuccess>> mapSuccessAsync)
        =>
        InnerMapSuccessValue(
            mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)));

    private AsyncPipeline<TResultSuccess, TFailure> InnerMapSuccessValue<TResultSuccess>(
        Func<TSuccess, CancellationToken, ValueTask<TResultSuccess>> mapSuccessAsync)
        =>
        InnerPipeValue(
            (result, cancellationToken) => result.MapSuccessValueAsync(
                success => mapSuccessAsync.Invoke(success, cancellationToken)));
}
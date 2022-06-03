using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess, TResultFailure> MapFailureValue<TResultFailure>(
        Func<TFailure, CancellationToken, ValueTask<TResultFailure>> mapFailureAsync)
        where TResultFailure : struct
        =>
        InnerMapFailureValue(
            mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

    private AsyncPipeline<TSuccess, TResultFailure> InnerMapFailureValue<TResultFailure>(
        Func<TFailure, CancellationToken, ValueTask<TResultFailure>> mapFailureAsync)
        where TResultFailure : struct
        =>
        InnerPipeValue(
            (result, cancellationToken) => result.MapFailureValueAsync(
                failure => mapFailureAsync.Invoke(failure, cancellationToken)));
}
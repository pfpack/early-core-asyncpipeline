using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess, TResultFailure> MapFailure<TResultFailure>(
        Func<TFailure, CancellationToken, Task<TResultFailure>> mapFailureAsync)
        where TResultFailure : struct
        =>
        InnerMapFailure(
            mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

    private AsyncPipeline<TSuccess, TResultFailure> InnerMapFailure<TResultFailure>(
        Func<TFailure, CancellationToken, Task<TResultFailure>> mapFailureAsync)
        where TResultFailure : struct
        =>
        InnerPipe(
            (result, cancellationToken) => result.MapFailureAsync(
                failure => mapFailureAsync.Invoke(failure, cancellationToken)));
}
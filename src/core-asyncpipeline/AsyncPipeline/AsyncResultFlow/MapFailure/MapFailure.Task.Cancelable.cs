using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncResultFlow<TSuccess, TFailure>
{
    public AsyncResultFlow<TSuccess, TResultFailure> MapFailure<TResultFailure>(
        Func<TFailure, CancellationToken, Task<TResultFailure>> mapFailureAsync)
        where TResultFailure : struct
        =>
        InnerMapFailure(
            mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

    private AsyncResultFlow<TSuccess, TResultFailure> InnerMapFailure<TResultFailure>(
        Func<TFailure, CancellationToken, Task<TResultFailure>> mapFailureAsync)
        where TResultFailure : struct
        =>
        InnerPipe(
            (result, cancellationToken) => result.MapFailureAsync(
                failure => mapFailureAsync.Invoke(failure, cancellationToken)));
}
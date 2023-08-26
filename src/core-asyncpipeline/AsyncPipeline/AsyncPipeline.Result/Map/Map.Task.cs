using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TResultSuccess, TResultFailure> Map<TResultSuccess, TResultFailure>(
        Func<TSuccess, CancellationToken, Task<TResultSuccess>> mapSuccessAsync,
        Func<TFailure, CancellationToken, Task<TResultFailure>> mapFailureAsync)
        where TResultFailure : struct
        =>
        InnerMap(
            mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)),
            mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

    private AsyncPipeline<TResultSuccess, TResultFailure> InnerMap<TResultSuccess, TResultFailure>(
        Func<TSuccess, CancellationToken, Task<TResultSuccess>> mapSuccessAsync,
        Func<TFailure, CancellationToken, Task<TResultFailure>> mapFailureAsync)
        where TResultFailure : struct
        =>
        InnerPipe(
            (r, t) => r.MapAsync(
                s => mapSuccessAsync.Invoke(s, t),
                f => mapFailureAsync.Invoke(f, t)));
}
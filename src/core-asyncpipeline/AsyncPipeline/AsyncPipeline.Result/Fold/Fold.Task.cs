using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TResult> Fold<TResult>(
        Func<TSuccess, CancellationToken, Task<TResult>> mapSuccessAsync,
        Func<TFailure, CancellationToken, Task<TResult>> mapFailureAsync)
        =>
        InnerFold(
            mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)),
            mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

    private AsyncPipeline<TResult> InnerFold<TResult>(
        Func<TSuccess, CancellationToken, Task<TResult>> mapSuccessAsync,
        Func<TFailure, CancellationToken, Task<TResult>> mapFailureAsync)
        =>
        pipeline.InternalPipe(
            (r, t) => r.FoldAsync(
                s => mapSuccessAsync.Invoke(s, t),
                f => mapFailureAsync.Invoke(f, t)));
}
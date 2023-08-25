using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TNextSuccess, TFailure> Forward<TNextSuccess, TNextFailure>(
        Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TNextFailure>>> nextAsync,
        Func<TNextFailure, TFailure> mapFailure)
        where TNextFailure : struct
        =>
        InnerForward(
            nextAsync ?? throw new ArgumentNullException(nameof(nextAsync)),
            mapFailure ?? throw new ArgumentNullException(nameof(mapFailure)));

    public AsyncPipeline<TNextSuccess, TFailure> Forward<TNextSuccess, TNextFailure>(
        Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TNextFailure>>> nextAsync,
        Func<TNextFailure, CancellationToken, Task<TFailure>> mapFailureAsync)
        where TNextFailure : struct
        =>
        InnerForward(
            nextAsync ?? throw new ArgumentNullException(nameof(nextAsync)),
            mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

    public AsyncPipeline<TNextSuccess, TNextFailure> Forward<TNextSuccess, TNextFailure>(
        Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TNextFailure>>> nextAsync,
        Func<TFailure, TNextFailure> mapFailure)
        where TNextFailure : struct
        =>
        InnerForward(
            nextAsync ?? throw new ArgumentNullException(nameof(nextAsync)),
            mapFailure ?? throw new ArgumentNullException(nameof(mapFailure)));

    public AsyncPipeline<TNextSuccess, TNextFailure> Forward<TNextSuccess, TNextFailure>(
        Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TNextFailure>>> nextAsync,
        Func<TFailure, CancellationToken, Task<TNextFailure>> mapFailureAsync)
        where TNextFailure : struct
        =>
        InnerForward(
            nextAsync ?? throw new ArgumentNullException(nameof(nextAsync)),
            mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

    public AsyncPipeline<TNextSuccess, TFailure> Forward<TNextSuccess>(
        Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TFailure>>> nextAsync)
        =>
        InnerForward(
            nextAsync ?? throw new ArgumentNullException(nameof(nextAsync)));

    private AsyncPipeline<TNextSuccess, TFailure> InnerForward<TNextSuccess, TNextFailure>(
        Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TNextFailure>>> nextAsync,
        Func<TNextFailure, TFailure> mapFailure)
        where TNextFailure : struct
    {
        var continueOnCapturedContext = pipeline.Configuration.ContinueOnCapturedContext;

        return InnerPipeValue(
            (r, t) => r.ForwardValueAsync(
                async s =>
                {
                    var next = await nextAsync.Invoke(s, t).ConfigureAwait(continueOnCapturedContext);
                    return next.MapFailure(mapFailure);
                }));
    }

    private AsyncPipeline<TNextSuccess, TFailure> InnerForward<TNextSuccess, TNextFailure>(
        Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TNextFailure>>> nextAsync,
        Func<TNextFailure, CancellationToken, Task<TFailure>> mapFailureAsync)
        where TNextFailure : struct
    {
        var continueOnCapturedContext = pipeline.Configuration.ContinueOnCapturedContext;

        return InnerPipeValue(
            (r, t) => r.ForwardValueAsync(
                async s =>
                {
                    var next = await nextAsync.Invoke(s, t).ConfigureAwait(false);
                    return await next.MapFailureAsync(f => mapFailureAsync.Invoke(f, t)).ConfigureAwait(continueOnCapturedContext);
                }));
    }

    private AsyncPipeline<TNextSuccess, TNextFailure> InnerForward<TNextSuccess, TNextFailure>(
        Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TNextFailure>>> nextAsync,
        Func<TFailure, TNextFailure> mapFailure)
        where TNextFailure : struct
        =>
        InnerPipe(
            (r, t) => r.ForwardAsync(
                s => nextAsync.Invoke(s, t),
                f => f.InternalPipe(mapFailure).InternalPipe(Task.FromResult)));

    private AsyncPipeline<TNextSuccess, TNextFailure> InnerForward<TNextSuccess, TNextFailure>(
        Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TNextFailure>>> nextAsync,
        Func<TFailure, CancellationToken, Task<TNextFailure>> mapFailureAsync)
        where TNextFailure : struct
        =>
        InnerPipe(
            (r, t) => r.ForwardAsync(
                s => nextAsync.Invoke(s, t),
                f => mapFailureAsync.Invoke(f, t)));

    private AsyncPipeline<TNextSuccess, TFailure> InnerForward<TNextSuccess>(
        Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TFailure>>> nextAsync)
        =>
        InnerPipe(
            (r, t) => r.ForwardAsync(
                s => nextAsync.Invoke(s, t)));
}
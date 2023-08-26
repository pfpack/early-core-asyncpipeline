using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess, TFailure> Recover(
        Func<TFailure, CancellationToken, Task<Result<TSuccess, TFailure>>> otherFactoryAsync)
    {
        _ = otherFactoryAsync ?? throw new ArgumentNullException(nameof(otherFactoryAsync));

        return InnerPipe(
            (current, token) => current.RecoverAsync(
                failure => otherFactoryAsync.Invoke(failure, token)));
    }

    public AsyncPipeline<TOtherSuccess, TFailure> Recover<TOtherSuccess>(
        Func<TFailure, CancellationToken, Task<Result<TOtherSuccess, TFailure>>> otherFactoryAsync,
        Func<TSuccess, CancellationToken, Task<TOtherSuccess>> mapSuccessAsync)
    {
        _ = otherFactoryAsync ?? throw new ArgumentNullException(nameof(otherFactoryAsync));
        _ = mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync));

        return InnerPipe(
            (current, token) => current.RecoverAsync(
                failure => otherFactoryAsync.Invoke(failure, token),
                success => mapSuccessAsync.Invoke(success, token)));
    }

    public AsyncPipeline<TSuccess, TOtherFailure> Recover<TOtherFailure>(
        Func<TFailure, CancellationToken, Task<Result<TSuccess, TOtherFailure>>> otherFactoryAsync)
        where TOtherFailure : struct
    {
        _ = otherFactoryAsync ?? throw new ArgumentNullException(nameof(otherFactoryAsync));

        return InnerPipe(
            (current, token) => current.RecoverAsync(
                failure => otherFactoryAsync.Invoke(failure, token)));
    }

    public AsyncPipeline<TOtherSuccess, TOtherFailure> Recover<TOtherSuccess, TOtherFailure>(
        Func<TFailure, CancellationToken, Task<Result<TOtherSuccess, TOtherFailure>>> otherFactoryAsync,
        Func<TSuccess, CancellationToken, Task<TOtherSuccess>> mapSuccessAsync)
        where TOtherFailure : struct
    {
        _ = otherFactoryAsync ?? throw new ArgumentNullException(nameof(otherFactoryAsync));
        _ = mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync));

        return InnerPipe(
            (current, token) => current.RecoverAsync(
                failure => otherFactoryAsync.Invoke(failure, token),
                success => mapSuccessAsync.Invoke(success, token)));
    }
}
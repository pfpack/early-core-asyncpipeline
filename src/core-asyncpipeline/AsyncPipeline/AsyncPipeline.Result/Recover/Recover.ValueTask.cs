using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TOtherSuccess, TFailure> RecoverValue<TOtherSuccess>(
        Func<TFailure, CancellationToken, ValueTask<Result<TOtherSuccess, TFailure>>> otherFactoryAsync,
        Func<TSuccess, CancellationToken, ValueTask<TOtherSuccess>> mapSuccessAsync)
    {
        _ = otherFactoryAsync ?? throw new ArgumentNullException(nameof(otherFactoryAsync));
        _ = mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync));

        return InnerPipeValue(
            (current, token) => current.RecoverValueAsync(
                failure => otherFactoryAsync.Invoke(failure, token),
                success => mapSuccessAsync.Invoke(success, token)));
    }

    public AsyncPipeline<TSuccess, TOtherFailure> RecoverValue<TOtherFailure>(
        Func<TFailure, CancellationToken, ValueTask<Result<TSuccess, TOtherFailure>>> otherFactoryAsync)
        where TOtherFailure : struct
    {
        _ = otherFactoryAsync ?? throw new ArgumentNullException(nameof(otherFactoryAsync));

        return InnerPipeValue(
            (current, token) => current.RecoverValueAsync(
                failure => otherFactoryAsync.Invoke(failure, token)));
    }

    public AsyncPipeline<TOtherSuccess, TOtherFailure> RecoverValue<TOtherSuccess, TOtherFailure>(
        Func<TFailure, CancellationToken, ValueTask<Result<TOtherSuccess, TOtherFailure>>> otherFactoryAsync,
        Func<TSuccess, CancellationToken, ValueTask<TOtherSuccess>> mapSuccessAsync)
        where TOtherFailure : struct
    {
        _ = otherFactoryAsync ?? throw new ArgumentNullException(nameof(otherFactoryAsync));
        _ = mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync));

        return InnerPipeValue(
            (current, token) => current.RecoverValueAsync(
                failure => otherFactoryAsync.Invoke(failure, token),
                success => mapSuccessAsync.Invoke(success, token)));
    }
}
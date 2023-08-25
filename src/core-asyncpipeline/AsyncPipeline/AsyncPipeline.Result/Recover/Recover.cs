namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TOtherSuccess, TFailure> Recover<TOtherSuccess>(
        Func<TFailure, Result<TOtherSuccess, TFailure>> otherFactory,
        Func<TSuccess, TOtherSuccess> mapSuccess)
    {
        _ = otherFactory ?? throw new ArgumentNullException(nameof(otherFactory));
        _ = mapSuccess ?? throw new ArgumentNullException(nameof(mapSuccess));

        return InnerPipe(
            current => current.Recover(otherFactory, mapSuccess));
    }

    public AsyncPipeline<TSuccess, TOtherFailure> Recover<TOtherFailure>(
        Func<TFailure, Result<TSuccess, TOtherFailure>> otherFactory)
        where TOtherFailure : struct
    {
        _ = otherFactory ?? throw new ArgumentNullException(nameof(otherFactory));

        return InnerPipe(
            current => current.Recover(otherFactory));
    }

    public AsyncPipeline<TOtherSuccess, TOtherFailure> Recover<TOtherSuccess, TOtherFailure>(
        Func<TFailure, Result<TOtherSuccess, TOtherFailure>> otherFactory,
        Func<TSuccess, TOtherSuccess> mapSuccess)
        where TOtherFailure : struct
    {
        _ = otherFactory ?? throw new ArgumentNullException(nameof(otherFactory));
        _ = mapSuccess ?? throw new ArgumentNullException(nameof(mapSuccess));

        return InnerPipe(
            current => current.Recover(otherFactory, mapSuccess));
    }
}
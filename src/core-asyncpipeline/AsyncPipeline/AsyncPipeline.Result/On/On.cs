namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess, TFailure> On(
        Action<TSuccess> onSuccess,
        Action<TFailure> onFailure)
    {
        _ = onSuccess ?? throw new ArgumentNullException(nameof(onSuccess));
        _ = onFailure ?? throw new ArgumentNullException(nameof(onFailure));

        return InnerMap(
            success =>
            {
                onSuccess.Invoke(success);
                return success;
            },
            failure =>
            {
                onFailure.Invoke(failure);
                return failure;
            });
    }

    public AsyncPipeline<TSuccess, TFailure> On(
        Func<TSuccess, Unit> onSuccess,
        Func<TFailure, Unit> onFailure)
    {
        _ = onSuccess ?? throw new ArgumentNullException(nameof(onSuccess));
        _ = onFailure ?? throw new ArgumentNullException(nameof(onFailure));

        return InnerMap(
            success =>
            {
                _ = onSuccess.Invoke(success);
                return success;
            },
            failure =>
            {
                _ = onFailure.Invoke(failure);
                return failure;
            });
    }
}
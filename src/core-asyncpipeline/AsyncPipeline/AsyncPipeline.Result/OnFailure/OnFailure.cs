namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess, TFailure> OnFailure(Action<TFailure> onFailure)
    {
        _ = onFailure ?? throw new ArgumentNullException(nameof(onFailure));

        return InnerMapFailure(
            failure =>
            {
                onFailure.Invoke(failure);
                return failure;
            });
    }

    public AsyncPipeline<TSuccess, TFailure> OnFailure(Func<TFailure, Unit> onFailure)
    {
        _ = onFailure ?? throw new ArgumentNullException(nameof(onFailure));

        return InnerMapFailure(
            failure =>
            {
                _ = onFailure.Invoke(failure);
                return failure;
            });
    }
}
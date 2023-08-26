namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess, TFailure> OnSuccess(Action<TSuccess> onSuccess)
    {
        _ = onSuccess ?? throw new ArgumentNullException(nameof(onSuccess));

        return InnerMapSuccess(
            success =>
            {
                onSuccess.Invoke(success);
                return success;
            });
    }

    public AsyncPipeline<TSuccess, TFailure> OnSuccess(Func<TSuccess, Unit> onSuccess)
    {
        _ = onSuccess ?? throw new ArgumentNullException(nameof(onSuccess));

        return InnerMapSuccess(
            success =>
            {
                _ = onSuccess.Invoke(success);
                return success;
            });
    }
}
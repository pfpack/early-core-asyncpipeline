using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess, TFailure> OnValue(
        Func<TSuccess, CancellationToken, ValueTask> onSuccessAsync,
        Func<TFailure, CancellationToken, ValueTask> onFailureAsync)
    {
        _ = onSuccessAsync ?? throw new ArgumentNullException(nameof(onSuccessAsync));
        _ = onFailureAsync ?? throw new ArgumentNullException(nameof(onFailureAsync));

        var continueOnCapturedContext = Configuration.ContinueOnCapturedContext;

        return InnerMapValue(
            async (success, token) =>
            {
                await onSuccessAsync.Invoke(success, token).ConfigureAwait(continueOnCapturedContext);
                return success;
            },
            async (failure, token) =>
            {
                await onFailureAsync.Invoke(failure, token).ConfigureAwait(continueOnCapturedContext);
                return failure;
            });
    }

    public AsyncPipeline<TSuccess, TFailure> OnValue(
        Func<TSuccess, CancellationToken, ValueTask<Unit>> onSuccessAsync,
        Func<TFailure, CancellationToken, ValueTask<Unit>> onFailureAsync)
    {
        _ = onSuccessAsync ?? throw new ArgumentNullException(nameof(onSuccessAsync));
        _ = onFailureAsync ?? throw new ArgumentNullException(nameof(onFailureAsync));

        var continueOnCapturedContext = Configuration.ContinueOnCapturedContext;

        return InnerMapValue(
            async (success, token) =>
            {
                _ = await onSuccessAsync.Invoke(success, token).ConfigureAwait(continueOnCapturedContext);
                return success;
            },
            async (failure, token) =>
            {
                _ = await onFailureAsync.Invoke(failure, token).ConfigureAwait(continueOnCapturedContext);
                return failure;
            });
    }
}
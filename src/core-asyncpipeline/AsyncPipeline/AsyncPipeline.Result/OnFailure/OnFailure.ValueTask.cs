using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess, TFailure> OnFailureValue(
        Func<TFailure, CancellationToken, ValueTask> onFailureAsync)
    {
        _ = onFailureAsync ?? throw new ArgumentNullException(nameof(onFailureAsync));

        var continueOnCapturedContext = Configuration.ContinueOnCapturedContext;

        return InnerMapFailureValue(
            async (failure, token) =>
            {
                await onFailureAsync.Invoke(failure, token).ConfigureAwait(continueOnCapturedContext);
                return failure;
            });
    }

    public AsyncPipeline<TSuccess, TFailure> OnFailureValue(
        Func<TFailure, CancellationToken, ValueTask<Unit>> onFailureAsync)
    {
        _ = onFailureAsync ?? throw new ArgumentNullException(nameof(onFailureAsync));

        var continueOnCapturedContext = Configuration.ContinueOnCapturedContext;

        return InnerMapFailureValue(
            async (failure, token) =>
            {
                _ = await onFailureAsync.Invoke(failure, token).ConfigureAwait(continueOnCapturedContext);
                return failure;
            });
    }
}
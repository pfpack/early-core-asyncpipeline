using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess, TFailure> OnFailure(
        Func<TFailure, CancellationToken, Task> onFailureAsync)
    {
        _ = onFailureAsync ?? throw new ArgumentNullException(nameof(onFailureAsync));

        var continueOnCapturedContext = Configuration.ContinueOnCapturedContext;

        return InnerMapFailure(
            async (failure, token) =>
            {
                await onFailureAsync.Invoke(failure, token).ConfigureAwait(continueOnCapturedContext);
                return failure;
            });
    }

    public AsyncPipeline<TSuccess, TFailure> OnFailure(
        Func<TFailure, CancellationToken, Task<Unit>> onFailureAsync)
    {
        _ = onFailureAsync ?? throw new ArgumentNullException(nameof(onFailureAsync));

        var continueOnCapturedContext = Configuration.ContinueOnCapturedContext;

        return InnerMapFailure(
            async (failure, token) =>
            {
                _ = await onFailureAsync.Invoke(failure, token).ConfigureAwait(continueOnCapturedContext);
                return failure;
            });
    }
}
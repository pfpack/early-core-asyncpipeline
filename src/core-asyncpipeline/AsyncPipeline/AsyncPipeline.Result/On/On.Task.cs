using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess, TFailure> On(
        Func<TSuccess, CancellationToken, Task> onSuccessAsync,
        Func<TFailure, CancellationToken, Task> onFailureAsync)
    {
        _ = onSuccessAsync ?? throw new ArgumentNullException(nameof(onSuccessAsync));
        _ = onFailureAsync ?? throw new ArgumentNullException(nameof(onFailureAsync));

        var continueOnCapturedContext = Configuration.ContinueOnCapturedContext;

        return InnerMap(
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

    public AsyncPipeline<TSuccess, TFailure> On(
        Func<TSuccess, CancellationToken, Task<Unit>> onSuccessAsync,
        Func<TFailure, CancellationToken, Task<Unit>> onFailureAsync)
    {
        _ = onSuccessAsync ?? throw new ArgumentNullException(nameof(onSuccessAsync));
        _ = onFailureAsync ?? throw new ArgumentNullException(nameof(onFailureAsync));

        var continueOnCapturedContext = Configuration.ContinueOnCapturedContext;

        return InnerMap(
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
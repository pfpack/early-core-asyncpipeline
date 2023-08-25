using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess, TFailure> OnSuccess(
        Func<TSuccess, CancellationToken, Task> onSuccessAsync)
    {
        _ = onSuccessAsync ?? throw new ArgumentNullException(nameof(onSuccessAsync));

        var continueOnCapturedContext = Configuration.ContinueOnCapturedContext;

        return InnerMapSuccess(
            async (success, token) =>
            {
                await onSuccessAsync.Invoke(success, token).ConfigureAwait(continueOnCapturedContext);
                return success;
            });
    }

    public AsyncPipeline<TSuccess, TFailure> OnSuccess(
        Func<TSuccess, CancellationToken, Task<Unit>> onSuccessAsync)
    {
        _ = onSuccessAsync ?? throw new ArgumentNullException(nameof(onSuccessAsync));

        var continueOnCapturedContext = Configuration.ContinueOnCapturedContext;

        return InnerMapSuccess(
            async (success, token) =>
            {
                _ = await onSuccessAsync.Invoke(success, token).ConfigureAwait(continueOnCapturedContext);
                return success;
            });
    }
}
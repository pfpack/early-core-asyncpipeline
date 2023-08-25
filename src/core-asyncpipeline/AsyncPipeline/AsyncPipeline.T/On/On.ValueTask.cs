using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public AsyncPipeline<T> OnValue(Func<T, CancellationToken, ValueTask> onAsync)
    {
        _ = onAsync ?? throw new ArgumentNullException(nameof(onAsync));
        var continueOnCapturedContext = Configuration.ContinueOnCapturedContext;

        return InternalPipeValue(
            async (current, token) =>
            {
                await onAsync.Invoke(current, token).ConfigureAwait(continueOnCapturedContext);
                return current;
            });
    }

    public AsyncPipeline<T> OnValue(Func<T, CancellationToken, ValueTask<Unit>> onAsync)
    {
        _ = onAsync ?? throw new ArgumentNullException(nameof(onAsync));
        var continueOnCapturedContext = Configuration.ContinueOnCapturedContext;

        return InternalPipeValue(
            async (current, token) =>
            {
                _ = await onAsync.Invoke(current, token).ConfigureAwait(continueOnCapturedContext);
                return current;
            });
    }
}
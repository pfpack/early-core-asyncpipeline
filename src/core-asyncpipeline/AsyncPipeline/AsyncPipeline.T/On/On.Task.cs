using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public AsyncPipeline<T> On(Func<T, CancellationToken, Task> onAsync)
    {
        _ = onAsync ?? throw new ArgumentNullException(nameof(onAsync));
        var continueOnCapturedContext = Configuration.ContinueOnCapturedContext;

        return InternalPipe(
            async (current, token) =>
            {
                await onAsync.Invoke(current, token).ConfigureAwait(continueOnCapturedContext);
                return current;
            });
    }

    public AsyncPipeline<T> On(Func<T, CancellationToken, Task<Unit>> onAsync)
    {
        _ = onAsync ?? throw new ArgumentNullException(nameof(onAsync));
        var continueOnCapturedContext = Configuration.ContinueOnCapturedContext;

        return InternalPipe(
            async (current, token) =>
            {
                _ = await onAsync.Invoke(current, token).ConfigureAwait(continueOnCapturedContext);
                return current;
            });
    }
}
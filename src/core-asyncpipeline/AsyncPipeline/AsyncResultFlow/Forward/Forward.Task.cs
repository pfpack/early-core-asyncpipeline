#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncResultFlow<TNextSuccess, TFailure> Forward<TNextSuccess, TNextFailure>(
            Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TNextFailure>>> nextAsync,
            Func<TNextFailure, TFailure> mapFailure)
            where TNextFailure : struct
            =>
            InnerForward(
                nextAsync ?? throw new ArgumentNullException(nameof(nextAsync)),
                mapFailure ?? throw new ArgumentNullException(nameof(mapFailure)));

        public AsyncResultFlow<TNextSuccess, TFailure> Forward<TNextSuccess, TNextFailure>(
            Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TNextFailure>>> nextAsync,
            Func<TNextFailure, CancellationToken, Task<TFailure>> mapFailureAsync)
            where TNextFailure : struct
            =>
            InnerForward(
                nextAsync ?? throw new ArgumentNullException(nameof(nextAsync)),
                mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

        public AsyncResultFlow<TNextSuccess, TNextFailure> Forward<TNextSuccess, TNextFailure>(
            Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TNextFailure>>> nextAsync,
            Func<TFailure, TNextFailure> mapFailure)
            where TNextFailure : struct
            =>
            InnerForward(
                nextAsync ?? throw new ArgumentNullException(nameof(nextAsync)),
                mapFailure ?? throw new ArgumentNullException(nameof(mapFailure)));

        public AsyncResultFlow<TNextSuccess, TNextFailure> Forward<TNextSuccess, TNextFailure>(
            Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TNextFailure>>> nextAsync,
            Func<TFailure, CancellationToken, Task<TNextFailure>> mapFailureAsync)
            where TNextFailure : struct
            =>
            InnerForward(
                nextAsync ?? throw new ArgumentNullException(nameof(nextAsync)),
                mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

        public AsyncResultFlow<TNextSuccess, TFailure> Forward<TNextSuccess>(
            Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TFailure>>> nextAsync)
            =>
            InnerForward(
                nextAsync ?? throw new ArgumentNullException(nameof(nextAsync)));

        private AsyncResultFlow<TNextSuccess, TFailure> InnerForward<TNextSuccess, TNextFailure>(
            Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TNextFailure>>> nextAsync,
            Func<TNextFailure, TFailure> mapFailure)
            where TNextFailure : struct
            =>
            InnerPipeValue(
                (r, t) => r.ForwardValueAsync(
                    async s =>
                    {
                        var next = await nextAsync.Invoke(s, t).ConfigureAwait(false);
                        return next.MapFailure(mapFailure);
                    }));

        private AsyncResultFlow<TNextSuccess, TFailure> InnerForward<TNextSuccess, TNextFailure>(
            Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TNextFailure>>> nextAsync,
            Func<TNextFailure, CancellationToken, Task<TFailure>> mapFailureAsync)
            where TNextFailure : struct
            =>
            InnerPipeValue(
                (r, t) => r.ForwardValueAsync(
                    async s =>
                    {
                        var next = await nextAsync.Invoke(s, t).ConfigureAwait(false);
                        return await next.MapFailureAsync(f => mapFailureAsync.Invoke(f, t)).ConfigureAwait(false);
                    }));

        private AsyncResultFlow<TNextSuccess, TNextFailure> InnerForward<TNextSuccess, TNextFailure>(
            Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TNextFailure>>> nextAsync,
            Func<TFailure, TNextFailure> mapFailure)
            where TNextFailure : struct
            =>
            InnerPipe(
                (r, t) => r.ForwardAsync(
                    s => nextAsync.Invoke(s, t),
                    f => f.InternalPipe(mapFailure).InternalPipe(Task.FromResult)));

        private AsyncResultFlow<TNextSuccess, TNextFailure> InnerForward<TNextSuccess, TNextFailure>(
            Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TNextFailure>>> nextAsync,
            Func<TFailure, CancellationToken, Task<TNextFailure>> mapFailureAsync)
            where TNextFailure : struct
            =>
            InnerPipe(
                (r, t) => r.ForwardAsync(
                    s => nextAsync.Invoke(s, t),
                    f => mapFailureAsync.Invoke(f, t)));

        private AsyncResultFlow<TNextSuccess, TFailure> InnerForward<TNextSuccess>(
            Func<TSuccess, CancellationToken, Task<Result<TNextSuccess, TFailure>>> nextAsync)
            =>
            InnerPipe(
                (r, t) => r.ForwardAsync(
                    s => nextAsync.Invoke(s, t)));
    }
}
#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncResultFlow<TNextSuccess, TFailure> ForwardValue<TNextSuccess, TNextFailure>(
            Func<TSuccess, CancellationToken, ValueTask<Result<TNextSuccess, TNextFailure>>> nextAsync,
            Func<TNextFailure, TFailure> mapFailure)
            where TNextFailure : struct
            =>
            InnerForwardValue(
                nextAsync ?? throw new ArgumentNullException(nameof(nextAsync)),
                mapFailure ?? throw new ArgumentNullException(nameof(mapFailure)));

        public AsyncResultFlow<TNextSuccess, TFailure> ForwardValue<TNextSuccess, TNextFailure>(
            Func<TSuccess, CancellationToken, ValueTask<Result<TNextSuccess, TNextFailure>>> nextAsync,
            Func<TNextFailure, CancellationToken, ValueTask<TFailure>> mapFailureAsync)
            where TNextFailure : struct
            =>
            InnerForwardValue(
                nextAsync ?? throw new ArgumentNullException(nameof(nextAsync)),
                mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

        public AsyncResultFlow<TNextSuccess, TNextFailure> ForwardValue<TNextSuccess, TNextFailure>(
            Func<TSuccess, CancellationToken, ValueTask<Result<TNextSuccess, TNextFailure>>> nextAsync,
            Func<TFailure, TNextFailure> mapFailure)
            where TNextFailure : struct
            =>
            InnerForwardValue(
                nextAsync ?? throw new ArgumentNullException(nameof(nextAsync)),
                mapFailure ?? throw new ArgumentNullException(nameof(mapFailure)));

        public AsyncResultFlow<TNextSuccess, TNextFailure> ForwardValue<TNextSuccess, TNextFailure>(
            Func<TSuccess, CancellationToken, ValueTask<Result<TNextSuccess, TNextFailure>>> nextAsync,
            Func<TFailure, CancellationToken, ValueTask<TNextFailure>> mapFailureAsync)
            where TNextFailure : struct
            =>
            InnerForwardValue(
                nextAsync ?? throw new ArgumentNullException(nameof(nextAsync)),
                mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

        public AsyncResultFlow<TNextSuccess, TFailure> ForwardValue<TNextSuccess>(
            Func<TSuccess, CancellationToken, ValueTask<Result<TNextSuccess, TFailure>>> nextAsync)
            =>
            InnerForwardValue(
                nextAsync ?? throw new ArgumentNullException(nameof(nextAsync)));

        private AsyncResultFlow<TNextSuccess, TFailure> InnerForwardValue<TNextSuccess, TNextFailure>(
            Func<TSuccess, CancellationToken, ValueTask<Result<TNextSuccess, TNextFailure>>> nextAsync,
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

        private AsyncResultFlow<TNextSuccess, TFailure> InnerForwardValue<TNextSuccess, TNextFailure>(
            Func<TSuccess, CancellationToken, ValueTask<Result<TNextSuccess, TNextFailure>>> nextAsync,
            Func<TNextFailure, CancellationToken, ValueTask<TFailure>> mapFailureAsync)
            where TNextFailure : struct
            =>
            InnerPipeValue(
                (r, t) => r.ForwardValueAsync(
                    async s =>
                    {
                        var next = await nextAsync.Invoke(s, t).ConfigureAwait(false);
                        return await next.MapFailureValueAsync(f => mapFailureAsync.Invoke(f, t)).ConfigureAwait(false);
                    }));

        private AsyncResultFlow<TNextSuccess, TNextFailure> InnerForwardValue<TNextSuccess, TNextFailure>(
            Func<TSuccess, CancellationToken, ValueTask<Result<TNextSuccess, TNextFailure>>> nextAsync,
            Func<TFailure, TNextFailure> mapFailure)
            where TNextFailure : struct
            =>
            InnerPipeValue(
                (r, t) => r.ForwardValueAsync(
                    s => nextAsync.Invoke(s, t),
                    f => f.InternalPipe(mapFailure).InternalPipe(ValueTask.FromResult)));

        private AsyncResultFlow<TNextSuccess, TNextFailure> InnerForwardValue<TNextSuccess, TNextFailure>(
            Func<TSuccess, CancellationToken, ValueTask<Result<TNextSuccess, TNextFailure>>> nextAsync,
            Func<TFailure, CancellationToken, ValueTask<TNextFailure>> mapFailureAsync)
            where TNextFailure : struct
            =>
            InnerPipeValue(
                (r, t) => r.ForwardValueAsync(
                    s => nextAsync.Invoke(s, t),
                    f => mapFailureAsync.Invoke(f, t)));

        private AsyncResultFlow<TNextSuccess, TFailure> InnerForwardValue<TNextSuccess>(
            Func<TSuccess, CancellationToken, ValueTask<Result<TNextSuccess, TFailure>>> nextAsync)
            =>
            InnerPipeValue(
                (r, t) => r.ForwardValueAsync(
                    s => nextAsync.Invoke(s, t)));
    }
}
#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncResultFlow<TResultSuccess, TResultFailure> Map<TResultSuccess, TResultFailure>(
            Func<TSuccess, CancellationToken, Task<TResultSuccess>> mapSuccessAsync,
            Func<TFailure, TResultFailure> mapFailure)
            where TResultFailure : struct
            =>
            InnerMap(
                mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)),
                mapFailure ?? throw new ArgumentNullException(nameof(mapFailure)));

        public AsyncResultFlow<TResultSuccess, TResultFailure> Map<TResultSuccess, TResultFailure>(
            Func<TSuccess, CancellationToken, Task<TResultSuccess>> mapSuccessAsync,
            Func<TFailure, CancellationToken, Task<TResultFailure>> mapFailureAsync)
            where TResultFailure : struct
            =>
            InnerMap(
                mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)),
                mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

        private AsyncResultFlow<TResultSuccess, TResultFailure> InnerMap<TResultSuccess, TResultFailure>(
            Func<TSuccess, CancellationToken, Task<TResultSuccess>> mapSuccessAsync,
            Func<TFailure, TResultFailure> mapFailure)
            where TResultFailure : struct
            =>
            InnerPipe(
                (r, t) => r.MapAsync(
                    s => mapSuccessAsync.Invoke(s, t),
                    f => f.InternalPipe(mapFailure).InternalPipe(Task.FromResult)));

        private AsyncResultFlow<TResultSuccess, TResultFailure> InnerMap<TResultSuccess, TResultFailure>(
            Func<TSuccess, CancellationToken, Task<TResultSuccess>> mapSuccessAsync,
            Func<TFailure, CancellationToken, Task<TResultFailure>> mapFailureAsync)
            where TResultFailure : struct
            =>
            InnerPipe(
                (r, t) => r.MapAsync(
                    s => mapSuccessAsync.Invoke(s, t),
                    f => mapFailureAsync.Invoke(f, t)));
    }
}
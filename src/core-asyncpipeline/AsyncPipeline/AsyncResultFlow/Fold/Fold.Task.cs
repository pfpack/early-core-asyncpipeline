#nullable enable

using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncPipeline<TResult> Fold<TResult>(
            Func<TSuccess, CancellationToken, Task<TResult>> mapSuccessAsync,
            Func<TFailure, TResult> mapFailure)
            =>
            InnerFilter(
                mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)),
                mapFailure ?? throw new ArgumentNullException(nameof(mapFailure)));

        public AsyncPipeline<TResult> Fold<TResult>(
            Func<TSuccess, CancellationToken, Task<TResult>> mapSuccessAsync,
            Func<TFailure, CancellationToken, Task<TResult>> mapFailureAsync)
            =>
            InnerFilter(
                mapSuccessAsync ?? throw new ArgumentNullException(nameof(mapSuccessAsync)),
                mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

        private AsyncPipeline<TResult> InnerFilter<TResult>(
            Func<TSuccess, CancellationToken, Task<TResult>> mapSuccessAsync,
            Func<TFailure, TResult> mapFailure)
            =>
            asyncPipeline.InternalPipe(
                (r, t) => r.FoldAsync(
                    s => mapSuccessAsync.Invoke(s, t),
                    f => f.Pipe(mapFailure).Pipe(Task.FromResult)));

        private AsyncPipeline<TResult> InnerFilter<TResult>(
            Func<TSuccess, CancellationToken, Task<TResult>> mapSuccessAsync,
            Func<TFailure, CancellationToken, Task<TResult>> mapFailureAsync)
            =>
            asyncPipeline.InternalPipe(
                (r, t) => r.FoldAsync(
                    s => mapSuccessAsync.Invoke(s, t),
                    f => mapFailureAsync.Invoke(f, t)));
    }
}
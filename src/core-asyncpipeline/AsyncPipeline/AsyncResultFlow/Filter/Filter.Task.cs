using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncResultFlow<TSuccess, TFailure> Filter(
            Func<TSuccess, CancellationToken, Task<bool>> predicateAsync,
            Func<TSuccess, TFailure> causeFactory)
            =>
            InnerFilter(
                predicateAsync ?? throw new ArgumentNullException(nameof(predicateAsync)),
                causeFactory ?? throw new ArgumentNullException(nameof(causeFactory)));

        public AsyncResultFlow<TSuccess, TFailure> Filter(
            Func<TSuccess, CancellationToken, Task<bool>> predicateAsync,
            Func<TSuccess, CancellationToken, Task<TFailure>> causeFactoryAsync)
            =>
            InnerFilter(
                predicateAsync ?? throw new ArgumentNullException(nameof(predicateAsync)),
                causeFactoryAsync ?? throw new ArgumentNullException(nameof(causeFactoryAsync)));

        private AsyncResultFlow<TSuccess, TFailure> InnerFilter(
            Func<TSuccess, CancellationToken, Task<bool>> predicateAsync,
            Func<TSuccess, TFailure> causeFactory)
            =>
            InnerPipe(
                (r, t) => r.FilterAsync(
                    s => predicateAsync.Invoke(s, t),
                    f => f.InternalPipe(causeFactory).InternalPipe(Task.FromResult)));

        private AsyncResultFlow<TSuccess, TFailure> InnerFilter(
            Func<TSuccess, CancellationToken, Task<bool>> predicateAsync,
            Func<TSuccess, CancellationToken, Task<TFailure>> causeFactoryAsync)
            =>
            InnerPipe(
                (r, t) => r.FilterAsync(
                    s => predicateAsync.Invoke(s, t),
                    f => causeFactoryAsync.Invoke(f, t)));
    }
}
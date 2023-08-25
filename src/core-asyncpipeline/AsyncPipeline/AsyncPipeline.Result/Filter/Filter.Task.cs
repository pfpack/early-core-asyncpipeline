using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess, TFailure> Filter(
        Func<TSuccess, CancellationToken, Task<bool>> predicateAsync,
        Func<TSuccess, TFailure> causeFactory)
        =>
        InnerFilter(
            predicateAsync ?? throw new ArgumentNullException(nameof(predicateAsync)),
            causeFactory ?? throw new ArgumentNullException(nameof(causeFactory)));

    public AsyncPipeline<TSuccess, TFailure> Filter(
        Func<TSuccess, CancellationToken, Task<bool>> predicateAsync,
        Func<TSuccess, CancellationToken, Task<TFailure>> causeFactoryAsync)
        =>
        InnerFilter(
            predicateAsync ?? throw new ArgumentNullException(nameof(predicateAsync)),
            causeFactoryAsync ?? throw new ArgumentNullException(nameof(causeFactoryAsync)));

    private AsyncPipeline<TSuccess, TFailure> InnerFilter(
        Func<TSuccess, CancellationToken, Task<bool>> predicateAsync,
        Func<TSuccess, TFailure> causeFactory)
        =>
        InnerPipe(
            (r, t) => r.FilterAsync(
                s => predicateAsync.Invoke(s, t),
                f => f.InternalPipe(causeFactory).InternalPipe(Task.FromResult)));

    private AsyncPipeline<TSuccess, TFailure> InnerFilter(
        Func<TSuccess, CancellationToken, Task<bool>> predicateAsync,
        Func<TSuccess, CancellationToken, Task<TFailure>> causeFactoryAsync)
        =>
        InnerPipe(
            (r, t) => r.FilterAsync(
                s => predicateAsync.Invoke(s, t),
                f => causeFactoryAsync.Invoke(f, t)));
}
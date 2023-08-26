using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess, TFailure> FilterValue(
        Func<TSuccess, CancellationToken, ValueTask<bool>> predicateAsync,
        Func<TSuccess, TFailure> causeFactory)
        =>
        InnerFilterValue(
            predicateAsync ?? throw new ArgumentNullException(nameof(predicateAsync)),
            causeFactory ?? throw new ArgumentNullException(nameof(causeFactory)));

    public AsyncPipeline<TSuccess, TFailure> FilterValue(
        Func<TSuccess, CancellationToken, ValueTask<bool>> predicateAsync,
        Func<TSuccess, CancellationToken, ValueTask<TFailure>> causeFactoryAsync)
        =>
        InnerFilterValue(
            predicateAsync ?? throw new ArgumentNullException(nameof(predicateAsync)),
            causeFactoryAsync ?? throw new ArgumentNullException(nameof(causeFactoryAsync)));

    private AsyncPipeline<TSuccess, TFailure> InnerFilterValue(
        Func<TSuccess, CancellationToken, ValueTask<bool>> predicateAsync,
        Func<TSuccess, TFailure> causeFactory)
        =>
        InnerPipeValue(
            (r, t) => r.FilterValueAsync(
                s => predicateAsync.Invoke(s, t),
                f => f.InternalPipe(causeFactory).InternalPipe(ValueTask.FromResult)));

    private AsyncPipeline<TSuccess, TFailure> InnerFilterValue(
        Func<TSuccess, CancellationToken, ValueTask<bool>> predicateAsync,
        Func<TSuccess, CancellationToken, ValueTask<TFailure>> causeFactoryAsync)
        =>
        InnerPipeValue(
            (r, t) => r.FilterValueAsync(
                s => predicateAsync.Invoke(s, t),
                f => causeFactoryAsync.Invoke(f, t)));
}
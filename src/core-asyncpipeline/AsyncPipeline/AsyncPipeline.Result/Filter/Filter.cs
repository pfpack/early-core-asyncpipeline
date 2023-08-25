namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess, TFailure> Filter(Func<TSuccess, bool> predicate, Func<TSuccess, TFailure> causeFactory)
        =>
        InnerFilter(
            predicate ?? throw new ArgumentNullException(nameof(predicate)),
            causeFactory ?? throw new ArgumentNullException(nameof(causeFactory)));

    private AsyncPipeline<TSuccess, TFailure> InnerFilter(Func<TSuccess, bool> predicate, Func<TSuccess, TFailure> causeFactory)
        =>
        InnerPipe(
            r => r.Filter(predicate, causeFactory));
}
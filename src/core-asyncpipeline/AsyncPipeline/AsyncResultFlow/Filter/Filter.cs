namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncResultFlow<TSuccess, TFailure> Filter(Func<TSuccess, bool> predicate, Func<TSuccess, TFailure> causeFactory)
            =>
            InnerFilter(
                predicate ?? throw new ArgumentNullException(nameof(predicate)),
                causeFactory ?? throw new ArgumentNullException(nameof(causeFactory)));

        private AsyncResultFlow<TSuccess, TFailure> InnerFilter(Func<TSuccess, bool> predicate, Func<TSuccess, TFailure> causeFactory)
            =>
            InnerPipe(
                r => r.Filter(predicate, causeFactory));
    }
}
namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncPipeline<TResult> Fold<TResult>(Func<TSuccess, TResult> mapSuccess, Func<TFailure, TResult> mapFailure)
            =>
            InnerFold(
                mapSuccess ?? throw new ArgumentNullException(nameof(mapSuccess)),
                mapFailure ?? throw new ArgumentNullException(nameof(mapFailure)));

        private AsyncPipeline<TResult> InnerFold<TResult>(Func<TSuccess, TResult> mapSuccess, Func<TFailure, TResult> mapFailure)
            =>
            asyncPipeline.InternalPipe(
                r => r.Fold(mapSuccess, mapFailure));
    }
}
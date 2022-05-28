namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncPipeline<TSuccess> SuccessOrThrow()
            =>
            asyncPipeline.InternalPipe(
                r => r.SuccessOrThrow());

        public AsyncPipeline<TSuccess> SuccessOrThrow(Func<Exception> exceptionFactory)
            =>
            InnerSuccessOrThrow(
                exceptionFactory ?? throw new ArgumentNullException(nameof(exceptionFactory)));

        private AsyncPipeline<TSuccess> InnerSuccessOrThrow(Func<Exception> exceptionFactory)
            =>
            asyncPipeline.InternalPipe(
                r => r.SuccessOrThrow(exceptionFactory));
    }
}
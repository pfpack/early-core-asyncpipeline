namespace System
{
    partial struct AsyncPipeline<TSuccess, TFailure>
    {
        public AsyncPipeline<TFailure> FailureOrThrow()
            =>
            pipeline.InternalPipe(
                r => r.FailureOrThrow());

        public AsyncPipeline<TFailure> FailureOrThrow(Func<Exception> exceptionFactory)
            =>
            InnerFailureOrThrow(
                exceptionFactory ?? throw new ArgumentNullException(nameof(exceptionFactory)));

        private AsyncPipeline<TFailure> InnerFailureOrThrow(Func<Exception> exceptionFactory)
            =>
            pipeline.InternalPipe(
                r => r.FailureOrThrow(exceptionFactory));
    }
}
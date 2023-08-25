namespace System
{
    partial struct AsyncPipeline<TSuccess, TFailure>
    {
        public AsyncPipeline<TSuccess> SuccessOrThrow()
            =>
            pipeline.InternalPipe(
                r => r.SuccessOrThrow());

        public AsyncPipeline<TSuccess> SuccessOrThrow(Func<Exception> exceptionFactory)
            =>
            InnerSuccessOrThrow(
                exceptionFactory ?? throw new ArgumentNullException(nameof(exceptionFactory)));

        private AsyncPipeline<TSuccess> InnerSuccessOrThrow(Func<Exception> exceptionFactory)
            =>
            pipeline.InternalPipe(
                r => r.SuccessOrThrow(exceptionFactory));
    }
}
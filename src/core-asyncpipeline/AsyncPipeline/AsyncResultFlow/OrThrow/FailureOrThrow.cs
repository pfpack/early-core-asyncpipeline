#nullable enable

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncPipeline<TFailure> FailureOrThrow()
            =>
            asyncPipeline.InternalPipe(
                r => r.FailureOrThrow());

        public AsyncPipeline<TFailure> FailureOrThrow(Func<Exception> exceptionFactory)
            =>
            InnerFailureOrThrow(
                exceptionFactory ?? throw new ArgumentNullException(nameof(exceptionFactory)));

        private AsyncPipeline<TFailure> InnerFailureOrThrow(Func<Exception> exceptionFactory)
            =>
            asyncPipeline.InternalPipe(
                r => r.FailureOrThrow(exceptionFactory));
    }
}
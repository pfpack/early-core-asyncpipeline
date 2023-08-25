namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TFailure> FailureOrThrow()
        =>
        pipeline.InternalPipe(
            r => r.FailureOrThrow());

    public AsyncPipeline<TFailure> FailureOrThrow(Func<Exception> exceptionFactory)
    {
        _ = exceptionFactory ?? throw new ArgumentNullException(nameof(exceptionFactory));

        return pipeline.InternalPipe(
            r => r.FailureOrThrow(exceptionFactory));
    }

    public AsyncPipeline<TFailure> FailureOrThrow(Func<TSuccess, Exception> exceptionFactory)
    {
        _ = exceptionFactory ?? throw new ArgumentNullException(nameof(exceptionFactory));

        return pipeline.InternalPipe(
            r => r.FailureOrThrow(exceptionFactory));
    }
}
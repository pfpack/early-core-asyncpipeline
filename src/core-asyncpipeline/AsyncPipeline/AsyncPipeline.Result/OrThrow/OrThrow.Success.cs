namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess> SuccessOrThrow()
        =>
        pipeline.InternalPipe(
            static r => r.SuccessOrThrow());

    public AsyncPipeline<TSuccess> SuccessOrThrow(Func<Exception> exceptionFactory)
    {
        _ = exceptionFactory ?? throw new ArgumentNullException(nameof(exceptionFactory));

        return pipeline.InternalPipe(
            r => r.SuccessOrThrow(exceptionFactory));
    }

    public AsyncPipeline<TSuccess> SuccessOrThrow(Func<TFailure, Exception> exceptionFactory)
    {
        _ = exceptionFactory ?? throw new ArgumentNullException(nameof(exceptionFactory));

        return pipeline.InternalPipe(
            r => r.SuccessOrThrow(exceptionFactory));
    }
}
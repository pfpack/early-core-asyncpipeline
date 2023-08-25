namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TResultSuccess, TResultFailure> Pipe<TResultSuccess, TResultFailure>(
        Func<Result<TSuccess, TFailure>, Result<TResultSuccess, TResultFailure>> pipe)
        where TResultFailure : struct
        =>
        InnerPipe(
            pipe ?? throw new ArgumentNullException(nameof(pipe)));

    private AsyncPipeline<TResultSuccess, TResultFailure> InnerPipe<TResultSuccess, TResultFailure>(
        Func<Result<TSuccess, TFailure>, Result<TResultSuccess, TResultFailure>> pipe)
        where TResultFailure : struct
        =>
        new(
            pipeline.InternalPipe(pipe));
}
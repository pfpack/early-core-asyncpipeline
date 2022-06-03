namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TResult> Pipe<TResult>(Func<Result<TSuccess, TFailure>, TResult> pipe)
        =>
        pipeline.InternalPipe(
            pipe ?? throw new ArgumentNullException(nameof(pipe)));
}
namespace System;

public readonly partial struct AsyncResultFlow<TSuccess, TFailure> : IEquatable<AsyncResultFlow<TSuccess, TFailure>>
    where TFailure : struct
{
    private readonly AsyncPipeline<Result<TSuccess, TFailure>> asyncPipeline;

    internal AsyncResultFlow(AsyncPipeline<Result<TSuccess, TFailure>> asyncPipeline)
        =>
        this.asyncPipeline = asyncPipeline;
}
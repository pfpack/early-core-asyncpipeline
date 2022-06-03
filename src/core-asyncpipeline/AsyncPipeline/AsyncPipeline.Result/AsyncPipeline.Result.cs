namespace System;

public readonly partial struct AsyncPipeline<TSuccess, TFailure> : IEquatable<AsyncPipeline<TSuccess, TFailure>>
    where TFailure : struct
{
    private readonly AsyncPipeline<Result<TSuccess, TFailure>> asyncPipeline;

    internal AsyncPipeline(AsyncPipeline<Result<TSuccess, TFailure>> asyncPipeline)
        =>
        this.asyncPipeline = asyncPipeline;
}
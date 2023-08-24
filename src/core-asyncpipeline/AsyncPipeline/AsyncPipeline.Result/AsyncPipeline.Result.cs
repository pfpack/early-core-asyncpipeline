namespace System;

public readonly partial struct AsyncPipeline<TSuccess, TFailure> : IEquatable<AsyncPipeline<TSuccess, TFailure>>
    where TFailure : struct
{
    private readonly AsyncPipeline<Result<TSuccess, TFailure>> pipeline;

    internal AsyncPipeline(AsyncPipeline<Result<TSuccess, TFailure>> pipeline)
        =>
        this.pipeline = pipeline;

    public AsyncPipelineOptions Options
        =>
        pipeline.Options;
}
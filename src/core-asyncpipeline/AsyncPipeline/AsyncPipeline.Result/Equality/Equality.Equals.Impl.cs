namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public bool Equals(AsyncPipeline<TSuccess, TFailure> other)
        =>
        AsyncPipelineComparer.Equals(asyncPipeline, other.asyncPipeline);
}
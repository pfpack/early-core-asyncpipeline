namespace System;

partial struct AsyncResultFlow<TSuccess, TFailure>
{
    public bool Equals(AsyncResultFlow<TSuccess, TFailure> other)
        =>
        AsyncPipelineComparer.Equals(asyncPipeline, other.asyncPipeline);
}
namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess, TFailure> HandleCancellation()
        =>
        new(pipeline.HandleCancellation());
}
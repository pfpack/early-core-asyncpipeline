namespace System;

partial struct AsyncResultFlow<TSuccess, TFailure>
{
    public AsyncResultFlow<TSuccess, TFailure> HandleCancellation()
        =>
        new(asyncPipeline.HandleCancellation());
}
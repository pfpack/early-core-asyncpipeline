namespace System;

partial struct AsyncPipeline<T>
{
    public AsyncPipeline<T> HandleCancellation()
        =>
        isStopped is false && cancellationToken.IsCancellationRequested ? new(default) : this;
}
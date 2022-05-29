namespace System;

partial struct AsyncPipeline<T>
{
    public AsyncPipeline<T> HandleCancellation()
        =>
        isCanceled is false && cancellationToken.IsCancellationRequested ? new(default) : this;
}
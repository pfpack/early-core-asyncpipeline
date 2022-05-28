namespace System;

partial struct AsyncPipeline<T>
{
    public AsyncPipeline<T> HandleCancellation()
        =>
        isCanceled || cancellationToken.IsCancellationRequested
            ? new(cancellationToken)
            : new(valueTask, cancellationToken);
}
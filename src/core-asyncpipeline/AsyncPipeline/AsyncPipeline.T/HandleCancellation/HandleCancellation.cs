namespace System;

partial struct AsyncPipeline<T>
{
    public AsyncPipeline<T> HandleCancellation()
        =>
        isCanceled || cancellationToken.IsCancellationRequested
            ? new(valueTask: default, isCanceled: true, cancellationToken: cancellationToken)
            : new(valueTask: valueTask, isCanceled: false, cancellationToken: cancellationToken);
}
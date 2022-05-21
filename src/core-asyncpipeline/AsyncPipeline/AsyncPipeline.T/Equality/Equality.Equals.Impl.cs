namespace System;

partial struct AsyncPipeline<T>
{
    public bool Equals(AsyncPipeline<T> other)
        =>
        ValueTaskComparer.Equals(valueTask, other.valueTask) &&
        IsCanceledComparer.Equals(isCanceled, other.isCanceled) &&
        CancellationTokenComparer.Equals(cancellationToken, other.cancellationToken);
}
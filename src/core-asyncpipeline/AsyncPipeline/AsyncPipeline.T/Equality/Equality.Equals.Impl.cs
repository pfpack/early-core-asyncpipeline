namespace System;

partial struct AsyncPipeline<T>
{
    public bool Equals(AsyncPipeline<T> other)
        =>
        IsCanceledComparer.Equals(isCanceled, other.isCanceled) &&
        ValueTaskComparer.Equals(valueTask, other.valueTask) &&
        CancellationTokenComparer.Equals(cancellationToken, other.cancellationToken);
}
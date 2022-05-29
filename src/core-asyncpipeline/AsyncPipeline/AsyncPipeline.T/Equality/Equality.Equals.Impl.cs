namespace System;

partial struct AsyncPipeline<T>
{
    public bool Equals(AsyncPipeline<T> other)
    {
        if (isCanceled != other.isCanceled)
        {
            return false;
        }

        if (isCanceled is false)
        {
            return
                ValueTaskComparer.Equals(valueTask, other.valueTask) &&
                CancellationTokenComparer.Equals(cancellationToken, other.cancellationToken);
        }

        return true;
    }
}
namespace System;

partial struct AsyncPipeline<T>
{
    public bool Equals(AsyncPipeline<T> other)
    {
        if (isStopped != other.isStopped)
        {
            return false;
        }

        if (isStopped is false)
        {
            return
                TaskComparer.Equals(task, other.task) &&
                CancellationTokenComparer.Equals(cancellationToken, other.cancellationToken);
        }

        return true;
    }
}
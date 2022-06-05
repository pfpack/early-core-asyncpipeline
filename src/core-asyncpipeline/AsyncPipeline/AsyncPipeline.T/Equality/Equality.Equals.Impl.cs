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
                TaskReferenceComparer.Equals(task, other.task) &&
                CancellationTokenComparer.Equals(cancellationToken, other.cancellationToken);
        }

        return true;
    }
}
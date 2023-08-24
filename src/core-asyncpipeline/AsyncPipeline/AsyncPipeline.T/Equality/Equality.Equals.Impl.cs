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
                ValueTaskComparer.Equals(valueTask, other.valueTask) &&
                ConfigurationComparer.Equals(Configuration, other.Configuration) &&
                CancellationTokenComparer.Equals(cancellationToken, other.cancellationToken);
        }

        return true;
    }
}
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
                OptionsComparer.Equals(Options, other.Options) &&
                CancellationTokenComparer.Equals(cancellationToken, other.cancellationToken);
        }

        return true;
    }
}
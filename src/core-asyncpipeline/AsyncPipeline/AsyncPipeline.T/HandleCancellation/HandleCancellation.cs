namespace System;

partial struct AsyncPipeline<T>
{
    public AsyncPipeline<T> HandleCancellation()
    {
        if (isCanceled)
        {
            return this;
        }

        if (cancellationToken.IsCancellationRequested)
        {
            return new(default);
        }

        return this;
    }
}
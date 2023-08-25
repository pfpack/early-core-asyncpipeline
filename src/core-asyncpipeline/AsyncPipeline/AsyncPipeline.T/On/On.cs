namespace System;

partial struct AsyncPipeline<T>
{
    public AsyncPipeline<T> On(Action<T> on)
    {
        _ = on ?? throw new ArgumentNullException(nameof(on));

        return InternalPipe(
            current =>
            {
                on.Invoke(current);
                return current;
            });
    }

    public AsyncPipeline<T> On(Func<T, Unit> on)
    {
        _ = on ?? throw new ArgumentNullException(nameof(on));

        return InternalPipe(
            current =>
            {
                _ = on.Invoke(current);
                return current;
            });
    }
}
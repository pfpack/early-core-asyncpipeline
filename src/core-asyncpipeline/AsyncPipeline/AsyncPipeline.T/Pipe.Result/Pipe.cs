namespace System
{
    partial struct AsyncPipeline<T>
    {
        public AsyncPipeline<TSuccess, TFailure> Pipe<TSuccess, TFailure>(Func<T, Result<TSuccess, TFailure>> pipe)
            where TFailure : struct
            =>
            new(
                InternalPipe(
                    pipe ?? throw new ArgumentNullException(nameof(pipe))));
    }
}
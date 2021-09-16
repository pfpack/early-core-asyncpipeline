#nullable enable

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public AsyncResultFlow<TSuccess, TFailure> Pipe<TSuccess, TFailure>(Func<T, Result<TSuccess, TFailure>> pipe)
            where TFailure : struct
            =>
            new(
                InternalPipe(
                    pipe ?? throw new ArgumentNullException(nameof(pipe))));
    }
}
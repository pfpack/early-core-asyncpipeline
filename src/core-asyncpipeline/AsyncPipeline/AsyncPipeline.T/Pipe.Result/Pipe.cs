#nullable enable

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public AsyncResultFlow<TSuccess, TFailure> Pipe<TSuccess, TFailure>(Func<T, Result<TSuccess, TFailure>> next)
            where TFailure : struct
            =>
            new(
                InternalPipe(
                    next ?? throw new ArgumentNullException(nameof(next))));
    }
}
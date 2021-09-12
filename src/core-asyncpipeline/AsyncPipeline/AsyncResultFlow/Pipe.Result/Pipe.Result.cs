#nullable enable

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncResultFlow<TResultSuccess, TResultFailure> Pipe<TResultSuccess, TResultFailure>(
            Func<Result<TSuccess, TFailure>, Result<TResultSuccess, TResultFailure>> map)
            where TResultFailure : struct
            =>
            InnerPipe(
                map ?? throw new ArgumentNullException(nameof(map)));

        private AsyncResultFlow<TResultSuccess, TResultFailure> InnerPipe<TResultSuccess, TResultFailure>(
            Func<Result<TSuccess, TFailure>, Result<TResultSuccess, TResultFailure>> map)
            where TResultFailure : struct
            =>
            new(
                asyncPipeline.InternalPipe(map));
    }
}
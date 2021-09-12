#nullable enable

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncPipeline<TResult> Pipe<TResult>(Func<Result<TSuccess, TFailure>, TResult> fold)
            =>
            asyncPipeline.InternalPipe(
                fold ?? throw new ArgumentNullException(nameof(fold)));
    }
}
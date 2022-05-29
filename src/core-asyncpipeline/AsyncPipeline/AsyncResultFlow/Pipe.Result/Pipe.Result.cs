namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public AsyncResultFlow<TResultSuccess, TResultFailure> Pipe<TResultSuccess, TResultFailure>(
            Func<Result<TSuccess, TFailure>, Result<TResultSuccess, TResultFailure>> pipe)
            where TResultFailure : struct
            =>
            InnerPipe(
                pipe ?? throw new ArgumentNullException(nameof(pipe)));

        private AsyncResultFlow<TResultSuccess, TResultFailure> InnerPipe<TResultSuccess, TResultFailure>(
            Func<Result<TSuccess, TFailure>, Result<TResultSuccess, TResultFailure>> pipe)
            where TResultFailure : struct
            =>
            new(
                asyncPipeline.InternalPipe(pipe));
    }
}
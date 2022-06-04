namespace System
{
    partial struct AsyncPipeline<TSuccess, TFailure>
    {
        public AsyncPipeline<TNextSuccess, TFailure> Forward<TNextSuccess, TNextFailure>(
            Func<TSuccess, Result<TNextSuccess, TNextFailure>> next,
            Func<TNextFailure, TFailure> mapFailure)
            where TNextFailure : struct
            =>
            InnerForward(
                next ?? throw new ArgumentNullException(nameof(next)),
                mapFailure ?? throw new ArgumentNullException(nameof(mapFailure)));

        public AsyncPipeline<TNextSuccess, TNextFailure> Forward<TNextSuccess, TNextFailure>(
            Func<TSuccess, Result<TNextSuccess, TNextFailure>> next,
            Func<TFailure, TNextFailure> mapFailure)
            where TNextFailure : struct
            =>
            InnerForward(
                next ?? throw new ArgumentNullException(nameof(next)),
                mapFailure ?? throw new ArgumentNullException(nameof(mapFailure)));

        public AsyncPipeline<TNextSuccess, TFailure> Forward<TNextSuccess>(
            Func<TSuccess, Result<TNextSuccess, TFailure>> next)
            =>
            InnerForward(
                next ?? throw new ArgumentNullException(nameof(next)));

        private AsyncPipeline<TNextSuccess, TFailure> InnerForward<TNextSuccess, TNextFailure>(
            Func<TSuccess, Result<TNextSuccess, TNextFailure>> next,
            Func<TNextFailure, TFailure> mapFailure)
            where TNextFailure : struct
            =>
            InnerPipe(
                r => r.Forward(
                    s => next.Invoke(s).MapFailure(mapFailure)));

        private AsyncPipeline<TNextSuccess, TNextFailure> InnerForward<TNextSuccess, TNextFailure>(
            Func<TSuccess, Result<TNextSuccess, TNextFailure>> next,
            Func<TFailure, TNextFailure> mapFailure)
            where TNextFailure : struct
            =>
            InnerPipe(
                r => r.Forward(next, mapFailure));

        private AsyncPipeline<TNextSuccess, TFailure> InnerForward<TNextSuccess>(
            Func<TSuccess, Result<TNextSuccess, TFailure>> next)
            =>
            InnerPipe(
                r => r.Forward(next));
    }
}
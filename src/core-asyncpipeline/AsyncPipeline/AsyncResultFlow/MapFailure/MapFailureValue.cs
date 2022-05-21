using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncResultFlow<TSuccess, TFailure>
{
    public AsyncResultFlow<TSuccess, TResultFailure> MapFailureValue<TResultFailure>(
        Func<TFailure, ValueTask<TResultFailure>> mapFailureAsync)
        where TResultFailure : struct
        =>
        InnerMapFailureValue(
            mapFailureAsync ?? throw new ArgumentNullException(nameof(mapFailureAsync)));

    private AsyncResultFlow<TSuccess, TResultFailure> InnerMapFailureValue<TResultFailure>(
        Func<TFailure, ValueTask<TResultFailure>> mapFailureAsync)
        where TResultFailure : struct
        =>
        new(
            asyncPipeline.InternalPipe(
                result => result.MapFailureValueAsync(mapFailureAsync)));
}
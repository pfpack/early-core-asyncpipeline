using System.Collections.Generic;

namespace System;

partial struct AsyncResultFlow<TSuccess, TFailure>
{
    private static Type EqualityContract
        =>
        typeof(AsyncResultFlow<TSuccess, TFailure>);

    private static EqualityComparer<Type> EqualityContractComparer
        =>
        EqualityComparer<Type>.Default;

    private static EqualityComparer<AsyncPipeline<Result<TSuccess, TFailure>>> AsyncPipelineComparer
        =>
        EqualityComparer<AsyncPipeline<Result<TSuccess, TFailure>>>.Default;
}
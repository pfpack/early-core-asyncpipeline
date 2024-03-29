using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    private static Type EqualityContract
        =>
        typeof(AsyncPipeline<T>);

    private static EqualityComparer<Type> EqualityContractComparer
        =>
        EqualityComparer<Type>.Default;

    private static EqualityComparer<ValueTask<T>> ValueTaskComparer
        =>
        EqualityComparer<ValueTask<T>>.Default;

    private static EqualityComparer<AsyncPipelineConfiguration> ConfigurationComparer
        =>
        EqualityComparer<AsyncPipelineConfiguration>.Default;

    private static EqualityComparer<CancellationToken> CancellationTokenComparer
        =>
        EqualityComparer<CancellationToken>.Default;
}
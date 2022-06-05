using System.Collections.Generic;
using System.Threading;

namespace System;

partial struct AsyncPipeline<T>
{
    private static Type EqualityContract
        =>
        typeof(AsyncPipeline<T>);

    private static EqualityComparer<Type> EqualityContractComparer
        =>
        EqualityComparer<Type>.Default;

    private static ReferenceEqualityComparer TaskReferenceComparer
        =>
        ReferenceEqualityComparer.Instance;

    private static EqualityComparer<CancellationToken> CancellationTokenComparer
        =>
        EqualityComparer<CancellationToken>.Default;
}
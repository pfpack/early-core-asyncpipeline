#nullable enable

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<T>
    {
        private static Type EqualityContract => typeof(AsyncPipeline<T>);

        private static IEqualityComparer<ValueTask<T>> ValueTaskComparer => EqualityComparer<ValueTask<T>>.Default;

        private static IEqualityComparer<bool> IsCanceledComparer => EqualityComparer<bool>.Default;

        private static IEqualityComparer<CancellationToken> CancellationTokenComparer => EqualityComparer<CancellationToken>.Default;
    }
}
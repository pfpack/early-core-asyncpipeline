#nullable enable

using System.Diagnostics.CodeAnalysis;

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public override bool Equals([NotNullWhen(true)] object? obj)
            =>
            obj is AsyncPipeline<T> other
            && Equals(other);
    }
}
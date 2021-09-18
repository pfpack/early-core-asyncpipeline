#nullable enable

using System.Diagnostics.CodeAnalysis;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public override bool Equals([NotNullWhen(true)] object? obj)
            =>
            obj is AsyncResultFlow<TSuccess, TFailure> other
            && Equals(other);
    }
}
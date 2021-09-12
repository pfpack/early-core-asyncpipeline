#nullable enable

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public override bool Equals(object? obj) => obj is AsyncResultFlow<TSuccess, TFailure> other && Equals(other);
    }
}
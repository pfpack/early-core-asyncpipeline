#nullable enable

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public static bool operator ==(AsyncResultFlow<TSuccess, TFailure> left, AsyncResultFlow<TSuccess, TFailure> right)
            =>
            left.Equals(right);

        public static bool operator !=(AsyncResultFlow<TSuccess, TFailure> left, AsyncResultFlow<TSuccess, TFailure> right)
            =>
            left.Equals(right) is false;
    }
}
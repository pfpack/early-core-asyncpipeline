#nullable enable

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public static bool Equals(AsyncResultFlow<TSuccess, TFailure> left, AsyncResultFlow<TSuccess, TFailure> right) => left.Equals(right);
    }
}
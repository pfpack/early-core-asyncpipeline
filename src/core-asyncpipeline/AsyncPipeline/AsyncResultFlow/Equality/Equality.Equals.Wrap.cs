using System.Diagnostics.CodeAnalysis;

namespace System;

partial struct AsyncResultFlow<TSuccess, TFailure>
{
    public static bool Equals(AsyncResultFlow<TSuccess, TFailure> left, AsyncResultFlow<TSuccess, TFailure> right)
        =>
        left.Equals(right);

    public static bool operator ==(AsyncResultFlow<TSuccess, TFailure> left, AsyncResultFlow<TSuccess, TFailure> right)
        =>
        left.Equals(right);

    public static bool operator !=(AsyncResultFlow<TSuccess, TFailure> left, AsyncResultFlow<TSuccess, TFailure> right)
        =>
        left.Equals(right) is false;

    public override bool Equals([NotNullWhen(true)] object? obj)
        =>
        obj is AsyncResultFlow<TSuccess, TFailure> other &&
        Equals(other);
}
using System.Diagnostics.CodeAnalysis;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public static bool Equals(AsyncPipeline<TSuccess, TFailure> left, AsyncPipeline<TSuccess, TFailure> right)
        =>
        left.Equals(right);

    public static bool operator ==(AsyncPipeline<TSuccess, TFailure> left, AsyncPipeline<TSuccess, TFailure> right)
        =>
        left.Equals(right);

    public static bool operator !=(AsyncPipeline<TSuccess, TFailure> left, AsyncPipeline<TSuccess, TFailure> right)
        =>
        left.Equals(right) is false;

    public override bool Equals([NotNullWhen(true)] object? obj)
        =>
        obj is AsyncPipeline<TSuccess, TFailure> other &&
        Equals(other);
}
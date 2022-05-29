using System.Diagnostics.CodeAnalysis;

namespace System;

partial struct AsyncPipeline<T>
{
    public static bool Equals(AsyncPipeline<T> left, AsyncPipeline<T> right)
        =>
        left.Equals(right);

    public static bool operator ==(AsyncPipeline<T> left, AsyncPipeline<T> right)
        =>
        left.Equals(right);

    public static bool operator !=(AsyncPipeline<T> left, AsyncPipeline<T> right)
        =>
        left.Equals(right) is false;

    public override bool Equals([NotNullWhen(true)] object? obj)
        =>
        obj is AsyncPipeline<T> other &&
        Equals(other);
}
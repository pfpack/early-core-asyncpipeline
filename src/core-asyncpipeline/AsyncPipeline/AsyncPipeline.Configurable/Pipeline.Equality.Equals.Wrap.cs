using System.Diagnostics.CodeAnalysis;

namespace System;

partial struct ConfigurableAsyncPipeline
{
    public static bool Equals(ConfigurableAsyncPipeline left, ConfigurableAsyncPipeline right)
        =>
        left.Equals(right);

    public static bool operator ==(ConfigurableAsyncPipeline left, ConfigurableAsyncPipeline right)
        =>
        left.Equals(right);

    public static bool operator !=(ConfigurableAsyncPipeline left, ConfigurableAsyncPipeline right)
        =>
        left.Equals(right) is false;

    public override bool Equals([NotNullWhen(true)] object? obj)
        =>
        obj is ConfigurableAsyncPipeline other &&
        Equals(other);
}
namespace System;

partial struct ConfigurableAsyncPipeline
{
    public bool Equals(ConfigurableAsyncPipeline other)
        =>
        ConfigurationComparer.Equals(Configuration, other.Configuration);
}
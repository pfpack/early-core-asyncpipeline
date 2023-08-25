namespace System;

public readonly partial struct ConfigurableAsyncPipeline : IEquatable<ConfigurableAsyncPipeline>
{
    private readonly AsyncPipelineConfiguration? configuration;

    internal ConfigurableAsyncPipeline(AsyncPipelineConfiguration? configuration)
        =>
        this.configuration = configuration;

    public AsyncPipelineConfiguration Configuration
        =>
        configuration ?? InnerDefaultConfiguration.Value;

    private static class InnerDefaultConfiguration
    {
        internal static readonly AsyncPipelineConfiguration Value = new();
    }
}
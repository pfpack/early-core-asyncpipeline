using System.Diagnostics.CodeAnalysis;

namespace System;

partial class AsyncPipeline
{
    public static ConfigurableAsyncPipeline Configure([AllowNull] AsyncPipelineConfiguration configuration)
        =>
        new(configuration);
}
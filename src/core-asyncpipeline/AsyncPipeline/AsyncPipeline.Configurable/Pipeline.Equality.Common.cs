using System.Collections.Generic;

namespace System;

partial struct ConfigurableAsyncPipeline
{
    private static Type EqualityContract
        =>
        typeof(ConfigurableAsyncPipeline);

    private static EqualityComparer<Type> EqualityContractComparer
        =>
        EqualityComparer<Type>.Default;

    private static EqualityComparer<AsyncPipelineConfiguration> ConfigurationComparer
        =>
        EqualityComparer<AsyncPipelineConfiguration>.Default;
}
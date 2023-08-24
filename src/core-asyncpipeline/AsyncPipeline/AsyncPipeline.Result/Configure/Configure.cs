using System.Diagnostics.CodeAnalysis;

namespace System;

partial struct AsyncPipeline<TSuccess, TFailure>
{
    public AsyncPipeline<TSuccess, TFailure> Configure([AllowNull] AsyncPipelineOptions options)
        =>
        new(
            pipeline.Configure(options));
}
using System.Diagnostics.CodeAnalysis;

namespace System;

partial struct AsyncPipeline<T>
{
    public AsyncPipeline<T> Configure([AllowNull] AsyncPipelineOptions options)
        =>
        isStopped is false
            ? new(valueTask, cancellationToken, options)
            : new(default);
}
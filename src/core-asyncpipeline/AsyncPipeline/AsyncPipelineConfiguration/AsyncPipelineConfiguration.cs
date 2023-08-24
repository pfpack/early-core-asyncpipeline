namespace System;

public sealed record class AsyncPipelineConfiguration
{
    public bool ContinueOnCapturedContext { get; init; }
}
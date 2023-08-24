namespace System;

public sealed record class AsyncPipelineOptions
{
    public bool ContinueOnCapturedContext { get; init; }
}
using System.Threading.Tasks;

namespace System;

partial struct AsyncPipeline<T>
{
    public Task<T> Build()
        =>
        isStopped is false ? InnerAwaitAsync() : Task.FromCanceled<T>(CanceledToken());

    public ValueTask<T> BuildValuePreserved()
        =>
        isStopped is false ? new(InnerAwaitAsync()) : ValueTask.FromCanceled<T>(CanceledToken());

    // TODO: Remove alternate redundant / less preferable implementations of the preserved (currently kept for development purposes):
    //isStopped is false ? new(InnerAwaitValueAsync().AsTask()) : ValueTask.FromCanceled<T>(CanceledToken());
    //isStopped is false ? InnerAwaitValueAsync().Preserve() : ValueTask.FromCanceled<T>(CanceledToken());
    //new(Build());
    //new(BuildValue().AsTask());
    //BuildValue().Preserve();

    public ValueTask<T> BuildValue()
        =>
        isStopped is false ? InnerAwaitValueAsync() : ValueTask.FromCanceled<T>(CanceledToken());

    // Note: inner awaits are used to avoid the leaky abstraction of the implementation design

    private async Task<T> InnerAwaitAsync()
        =>
        await GetTask().ConfigureAwait(false);

    private async ValueTask<T> InnerAwaitValueAsync()
        =>
        await GetTask().ConfigureAwait(false);
}
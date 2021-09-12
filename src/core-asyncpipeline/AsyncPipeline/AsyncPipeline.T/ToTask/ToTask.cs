#nullable enable

using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public Task<T> ToTask()
            =>
            hasCanceled is false ? valueTask.AsTask() : Task.FromCanceled<T>(CanceledToken);
    }
}
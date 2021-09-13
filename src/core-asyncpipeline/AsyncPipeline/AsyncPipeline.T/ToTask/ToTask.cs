#nullable enable

using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public Task<T> ToTask()
            =>
            isCanceled is false ? valueTask.AsTask() : Task.FromCanceled<T>(CanceledToken());
    }
}
#nullable enable

using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public ValueTask<T> ToValueTask()
            =>
            hasCanceled is false ? valueTask : ValueTask.FromCanceled<T>(CanceledToken);
    }
}
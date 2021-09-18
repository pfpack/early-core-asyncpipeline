#nullable enable

using System.Threading.Tasks;

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public static implicit operator Task<T>(AsyncPipeline<T> pipeline)
            =>
            pipeline.ToTask();
    }
}
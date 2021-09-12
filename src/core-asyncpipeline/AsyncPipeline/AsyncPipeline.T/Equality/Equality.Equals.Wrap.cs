#nullable enable

namespace System
{
    partial struct AsyncPipeline<T>
    {
        public override bool Equals(object? obj)
            =>
            obj is AsyncPipeline<T> other
            && Equals(other);
    }
}
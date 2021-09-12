#nullable enable

using System.Threading.Tasks;

namespace System
{
    partial struct AsyncResultFlow<TSuccess, TFailure>
    {
        public ValueTask<Result<TSuccess, TFailure>> ToValueTask()
            =>
            asyncPipeline.ToValueTask();
    }
}
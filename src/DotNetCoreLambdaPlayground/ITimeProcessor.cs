using System;

namespace DotNetCoreLambdaPlayground
{
    public interface ITimeProcessor
    {
        DateTime CurrentTimeUtc();
    }
}
using System;

namespace DotNetCoreLambdaPlayground
{
    public class TimeProcessor : ITimeProcessor
    {
        public DateTime CurrentTimeUtc() => DateTime.UtcNow;
    }
}
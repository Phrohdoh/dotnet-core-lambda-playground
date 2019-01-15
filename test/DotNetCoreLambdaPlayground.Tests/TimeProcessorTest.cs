using System;
using Xunit;

namespace DotNetCoreLambdaPlayground.Tests
{
    public class TimeProcessorTest
    {
        [Fact]
        public void TestCurrentTimeUtc()
        {
            // Arrange
            var proc = new TimeProcessor();
            var preTestTimeUtc = DateTime.UtcNow;

            // Act
            var res = proc.CurrentTimeUtc();

            // Assert
            var postTestTimeUtc = DateTime.UtcNow;

            Assert.True(res >= preTestTimeUtc);
            Assert.True(res <= postTestTimeUtc);
        }
    }
}
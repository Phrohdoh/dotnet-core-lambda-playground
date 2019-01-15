using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace DotNetCoreLambdaPlayground
{
    public class Functions
    {
        ITimeProcessor proc = new TimeProcessor();

        /// <summary>
        /// Default constructor that Lambda will invoke.
        /// </summary>
        public Functions()
        {
        }

        /// <summary>
        /// A Lambda function to respond to HTTP Get methods from API Gateway
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The list of blogs</returns>
        public APIGatewayProxyResponse Get(APIGatewayProxyRequest request, ILambdaContext context)
        {
            LogMessage(context, "Processing request started");

            APIGatewayProxyResponse resp;

            try
            {
                var curTimeUtc = proc.CurrentTimeUtc();
                resp = CreateResponse(curTimeUtc);
                LogMessage(context, "Processing request succeeded");
            }
            catch (Exception ex)
            {
                LogMessage(context, $"Processing request failed - {ex.Message}");
                resp = CreateResponse(null);
            }

            return resp;
        }

        private APIGatewayProxyResponse CreateResponse(DateTime? curTimeUtc)
        {
            var statusCode = curTimeUtc == null ? HttpStatusCode.InternalServerError : HttpStatusCode.OK;
            var body = curTimeUtc == null ? string.Empty : JsonConvert.SerializeObject(curTimeUtc);

            var resp = new APIGatewayProxyResponse
            {
                StatusCode = (int)statusCode,
                Body = body,
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" },
                    { "Access-Control-Allow-Origin", "*" },
                },
            };

            return resp;
        }

        void LogMessage(ILambdaContext ctx, string msg) => ctx.Logger.LogLine($"{ctx.AwsRequestId}:{ctx.FunctionName} - {msg}");
    }
}

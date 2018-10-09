// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace HelloProj
{
    public static class HelloOrchestratorClient
    {
        private const string Timeout = "timeout";
        private const string RetryInterval = "retryInterval";

        private const string apiHeaderKey = "x-apikey";
        private const string APIVersion = "apiVersion";
        private const string APIName = "api";

        [FunctionName("HttpSyncStart")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, methods: "get", Route = "orchestrators/{functionName}/wait")]
            HttpRequestMessage req,
            [OrchestrationClient] DurableOrchestrationClient starter,
            string functionName,
            ILogger log)
        {
            // Function input comes from the request content.
            dynamic eventData = await req.Content.ReadAsAsync<object>();
            HttpReq originalRequest = new HttpReq() {
                    DeveloperId = GetDevKey(req,apiHeaderKey),
                    QueryString = req.RequestUri.Query,
                    APIName = GetQueryStringValue(req,APIName),
                    APIVersion = GetQueryStringValue(req,APIVersion)

            };
            string instanceId =   await starter.StartNewAsync(functionName, originalRequest);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            TimeSpan timeout = GetTimeSpan(req, Timeout) ?? TimeSpan.FromSeconds(30);
            TimeSpan retryInterval = GetTimeSpan(req, RetryInterval) ?? TimeSpan.FromSeconds(1);
        
            return  await starter.WaitForCompletionOrCreateCheckStatusResponseAsync(
                req,
                instanceId,
                timeout,
                retryInterval);

        }

        private static TimeSpan? GetTimeSpan(HttpRequestMessage request, string queryParameterName)
        {
            string queryParameterStringValue = request.RequestUri.ParseQueryString()[queryParameterName];
            if (string.IsNullOrEmpty(queryParameterStringValue))
            {
                return null;
            }

            return TimeSpan.FromSeconds(double.Parse(queryParameterStringValue));
        }

        private static string GetQueryStringValue(HttpRequestMessage request, string queryParameterName)
        {
            string queryParameterStringValue = request.RequestUri.ParseQueryString()[queryParameterName];
            if (string.IsNullOrEmpty(queryParameterStringValue))
            {
                return null;
            }
            return queryParameterStringValue;
        }

        private static string GetDevKey(HttpRequestMessage request, string key)
        {

            IEnumerable<string> keyValue = Enumerable.Empty<string>();
            var found = request.Headers.TryGetValues(key,out keyValue);

            if(found)
            {
                return keyValue.FirstOrDefault();
            }
            else
            {
                return string.Empty;
            }
        }
    }

}
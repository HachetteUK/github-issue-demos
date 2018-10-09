using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using HelloProj.CosmosDB.Models.Products;
using HelloProj.CosmosDB.Models;

namespace HelloProj
{
public static class HelloOrchestrator
{
        private static HttpClient client = new HttpClient();

        private const string baseAddress = "https://huxgendcsapidev2-dev.azurewebsites.net/api/";
        [FunctionName("E1_JsonProduct")]
        public static async Task<List<JsonProduct>> Run(
            [OrchestrationTrigger] DurableOrchestrationContextBase context,
            ILogger log)
        {
            List<JsonProduct> output = new List<JsonProduct>();
            HttpReq r = context.GetInput<HttpReq>();
            //JsonProduct response = null;
            if(r != null)
            {
                if(r.DeveloperId == null)
                {
                    return output;
                }
                //response =  await context.CallActivityAsync<JsonProduct>("E1_CallAPI",r);
                output.Add(await context.CallActivityAsync<JsonProduct>("E1_CallAPI",r));
                return output;
            }
            return output;
        } 

        [FunctionName("E1_CallAPI")]
        public async static Task<JsonProduct> APICall([ActivityTrigger] HttpReq req,
            ILogger log)
        {

            JsonProduct products  = null;
            string u = $"{baseAddress}{req.APIVersion}/{req.APIName}{req.QueryString}";  

            var request = new HttpRequestMessage(HttpMethod.Get, u);
            request.Headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
            request.Headers.Add("x-apikey",req.DeveloperId);
             log.LogInformation($"URL calling = '{request.RequestUri.AbsoluteUri}'.");
            HttpResponseMessage response = await client.SendAsync(request);
            // return await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
            {
                var formatter = new JsonMediaTypeFormatter
                {
                    SerializerSettings = HelloProj.CosmosDB.Models.Products.Converter.Settings
                };

                products = await response.Content.ReadAsAsync<JsonProduct>(new [] {formatter});
            }
            return products;
        }
    }
}
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace HelloProj
{
    public static class HelloOrchestrator
{
        private static HttpClient client = new HttpClient();

       [FunctionName("E1_Todo")]
        public static async Task<TestToDo> RunToDo(
        [OrchestrationTrigger] DurableOrchestrationContextBase context,
            ILogger log)
        {
            HttpReq r = context.GetInput<HttpReq>();
            TestToDo todo = new TestToDo();
            if(r != null)
            {
                todo = await context.CallActivityAsync<TestToDo>("E1_CallAPITodo",r);
            }
            return todo;
        }


        [FunctionName("E1_CallAPITodo")]
        public async static Task<TestToDo> APITodoCall([ActivityTrigger] HttpReq req,
            ILogger log)
        {
  
            var request = new HttpRequestMessage(HttpMethod.Get, "https://jsonplaceholder.typicode.com/todos/1");
            request.Headers.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );
             log.LogInformation($"URL calling = '{request.RequestUri.AbsoluteUri}'. for {req.QueryString}");
            HttpResponseMessage response = await client.SendAsync(request);
            return await response.Content.ReadAsAsync<TestToDo>();
        }
    }
}
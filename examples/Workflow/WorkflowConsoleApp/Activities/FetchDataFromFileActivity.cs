using Dapr.Client;
using Dapr.Workflow;
using Microsoft.Extensions.Logging;
using WorkflowConsoleApp.Models;

namespace WorkflowConsoleApp.Activities
{
    public class FetchDataFromFileActivity : WorkflowActivity<string, List<string>>
    {
        public override Task<List<string>> RunAsync(WorkflowActivityContext context, string input)
        {
            Console.WriteLine("****************** Fetch Rows from File ******************");

            var file = File.ReadAllLines(input);
            var vmIds = new List<string>(file);

            Console.WriteLine("****************** Completed fetching Rows from File ******************");

            return Task.FromResult(vmIds);
        }
    }
}

using Dapr.Client;
using Dapr.Workflow;
using Microsoft.Extensions.Logging;
using WorkflowConsoleApp.Models;

namespace WorkflowConsoleApp.Activities
{
    public class VMMetadataFetchActivity : WorkflowActivity<string, string>
    {
        public override Task<string> RunAsync(WorkflowActivityContext context, string input)
        {
            Console.WriteLine("****************** Fetch VM Metadata ******************");
            Console.WriteLine("VM Metadata for: " + input);

            return Task.FromResult("service:" + input);
        }
    }
}

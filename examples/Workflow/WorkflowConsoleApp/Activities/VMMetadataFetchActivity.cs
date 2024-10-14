using Dapr.Client;
using Dapr.Workflow;
using Microsoft.Extensions.Logging;
using WorkflowConsoleApp.Models;

namespace WorkflowConsoleApp.Activities
{
    public class VMMetadataFetchActivity : WorkflowActivity<string, bool>
    {
        public override Task<bool> RunAsync(WorkflowActivityContext context, string input)
        {
            Console.WriteLine("****************** Fetch VM Metadata ******************");
            Console.WriteLine("VM Metadata for: " + input);

            return Task.FromResult(true);
        }
    }
}

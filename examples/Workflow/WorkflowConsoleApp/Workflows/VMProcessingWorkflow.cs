using Dapr.Workflow;
using WorkflowConsoleApp.Activities;
using WorkflowConsoleApp.Models;

namespace WorkflowConsoleApp.Workflows
{
    public class VMProcessingWorkflow : Workflow<string, bool>
    {
        public override async Task<bool> RunAsync(WorkflowContext context, string filename)
        {
            string[] vms = { "vm-111", "vm-222", "vm-333" };

            await context.CallActivityAsync(nameof(VMMetadataFetchActivity), vms[0]);

            await context.CallActivityAsync(nameof(VMMetadataFetchActivity), vms[1]);

            return true;
        }
    }
}

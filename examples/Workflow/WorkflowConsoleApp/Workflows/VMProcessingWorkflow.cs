using Dapr.Workflow;
using WorkflowConsoleApp.Activities;
using WorkflowConsoleApp.Models;

namespace WorkflowConsoleApp.Workflows
{
    public class VMProcessingWorkflow : Workflow<string, bool>
    {
        public override async Task<bool> RunAsync(WorkflowContext context, string filePath)
        {
            List<string> vmIds = await context.CallActivityAsync<List<string>>(nameof(FetchDataFromFileActivity), filePath);

            List<string> serviceDetails = new List<string>();
            foreach (string vmId in vmIds)
            {
                serviceDetails.Add(await context.CallActivityAsync<string>(nameof(VMMetadataFetchActivity), vmId));
            }

            return true;
        }
    }
}

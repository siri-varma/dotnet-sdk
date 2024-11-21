using Dapr.Client;
using Dapr.Workflow;
using WorkflowConsoleApp.Activities;
using WorkflowConsoleApp.Workflows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddDaprWorkflow(options =>
    {
        options.RegisterWorkflow<VMProcessingWorkflow>();

        options.RegisterActivity<FetchDataFromFileActivity>();
        options.RegisterActivity<VMMetadataFetchActivity>();
    });
});

Environment.SetEnvironmentVariable("DAPR_GRPC_PORT", "4001");

using var host = builder.Build();
host.Start();
DaprClient daprClient = new DaprClientBuilder().Build();
Thread.Sleep(TimeSpan.FromSeconds(1));

// Get the client
var daprWorkflowClient = host.Services.GetRequiredService<DaprWorkflowClient>();

string workflowName = "workflow-23467";

// Schedule the workflow
// await daprWorkflowClient.ScheduleNewWorkflowAsync(name: nameof(VMProcessingWorkflow), input: "C:\\Users\\svegiraju\\OneDrive - Microsoft\\Desktop\\vms.txt", instanceId: workflowName);

WorkflowState state = await daprWorkflowClient.WaitForWorkflowStartAsync(instanceId: workflowName);

// Wait until the workflow completes
while (true)
{
    state = await daprWorkflowClient.WaitForWorkflowCompletionAsync(instanceId: workflowName, default);

    if (state.IsWorkflowCompleted)
    {
        break;
    }
}

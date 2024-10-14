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

        options.RegisterActivity<VMMetadataFetchActivity>();
    });
});

Environment.SetEnvironmentVariable("DAPR_GRPC_PORT", "4001");

using var host = builder.Build();
host.Start();
DaprClient daprClient = new DaprClientBuilder().Build();
Thread.Sleep(TimeSpan.FromSeconds(1));


var daprWorkflowClient = host.Services.GetRequiredService<DaprWorkflowClient>();

// await daprWorkflowClient.ScheduleNewWorkflowAsync(name: nameof(VMProcessingWorkflow), input: "string", instanceId: "workflow-4");

WorkflowState state = await daprWorkflowClient.WaitForWorkflowStartAsync(instanceId: "workflow-4");

while (true)
{
    state = await daprWorkflowClient.WaitForWorkflowCompletionAsync(instanceId: "workflow-4", default);

    if (state.IsWorkflowCompleted)
    {
        break;
    }
}

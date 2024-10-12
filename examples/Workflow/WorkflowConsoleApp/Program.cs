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
        options.RegisterWorkflow<OrderProcessingWorkflow>();

        options.RegisterActivity<DebitCashActivity>();
        options.RegisterActivity<CreditCashActivity>();
    });
});

Environment.SetEnvironmentVariable("DAPR_GRPC_PORT", "4002");

using var host = builder.Build();
host.Start();
DaprClient daprClient = new DaprClientBuilder().Build();
Thread.Sleep(TimeSpan.FromSeconds(1));


var daprWorkflowClient = host.Services.GetRequiredService<DaprWorkflowClient>();

// await daprWorkflowClient.ScheduleNewWorkflowAsync(name: nameof(OrderProcessingWorkflow), input: "string", instanceId: "workflow-1");

WorkflowState state = await daprWorkflowClient.WaitForWorkflowStartAsync(instanceId: "workflow-1");

while (true)
{
    state = await daprWorkflowClient.WaitForWorkflowCompletionAsync(instanceId: "workflow-1", default);

    if (state.IsWorkflowCompleted)
    {
        break;
    }
}

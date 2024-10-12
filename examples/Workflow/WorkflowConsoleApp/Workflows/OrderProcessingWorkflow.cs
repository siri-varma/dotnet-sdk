using Dapr.Workflow;
using WorkflowConsoleApp.Activities;
using WorkflowConsoleApp.Models;

namespace WorkflowConsoleApp.Workflows
{
    public class OrderProcessingWorkflow : Workflow<string, bool>
    {
        public override async Task<bool> RunAsync(WorkflowContext context, string orderId)
        {
            // Notify the user that an order has come through
            await context.CallActivityAsync(nameof(DebitCashActivity), 50);

            await context.CallActivityAsync(nameof(CreditCashActivity), 50);

            return true;
        }
    }
}

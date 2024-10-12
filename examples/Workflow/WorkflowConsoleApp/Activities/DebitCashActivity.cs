using Dapr.Client;
using Dapr.Workflow;
using Microsoft.Extensions.Logging;
using WorkflowConsoleApp.Models;

namespace WorkflowConsoleApp.Activities
{
    public class DebitCashActivity : WorkflowActivity<int, bool>
    {
        public override Task<bool> RunAsync(WorkflowActivityContext context, int input)
        {
            Console.WriteLine("****************** Debit Activity ******************");
            Console.WriteLine("Debited Cash: " + input);

            return Task.FromResult(true);
        }
    }
}

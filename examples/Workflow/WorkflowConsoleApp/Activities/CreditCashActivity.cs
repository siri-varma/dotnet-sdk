using Dapr.Client;
using Dapr.Workflow;
using Microsoft.Extensions.Logging;
using WorkflowConsoleApp.Models;

namespace WorkflowConsoleApp.Activities
{
    public class CreditCashActivity : WorkflowActivity<int, bool>
    {
        public override Task<bool> RunAsync(WorkflowActivityContext context, int input)
        {
            Console.WriteLine("******************* Credit Cash Activity******************");
            Console.WriteLine("Credited Cash: " + input);

            return Task.FromResult(true);
        }
    }
}

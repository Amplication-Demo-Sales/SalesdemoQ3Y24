using System.Threading.Tasks;
using CrmService.Brokers.Infrastructure;

namespace CrmService.Brokers.Mymessagebroker;

public class MymessagebrokerMessageHandlersController
{
    [Topic("updatebooking")]
    public Task HandleUpdatebooking(string message)
    {
        //set your message handling logic here

        return Task.CompletedTask;
    }

    [Topic("updateblog")]
    public Task HandleUpdateblog(string message)
    {
        //set your message handling logic here

        return Task.CompletedTask;
    }
}

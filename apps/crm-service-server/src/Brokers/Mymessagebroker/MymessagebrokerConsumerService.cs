using CrmService.Brokers.Infrastructure;
using CrmService.Brokers.Mymessagebroker;
using Microsoft.Extensions.DependencyInjection;

namespace CrmService.Brokers.Mymessagebroker;

public class MymessagebrokerConsumerService
    : KafkaConsumerService<MymessagebrokerMessageHandlersController>
{
    public MymessagebrokerConsumerService(
        IServiceScopeFactory serviceScopeFactory,
        KafkaOptions kafkaOptions
    )
        : base(serviceScopeFactory, kafkaOptions) { }
}

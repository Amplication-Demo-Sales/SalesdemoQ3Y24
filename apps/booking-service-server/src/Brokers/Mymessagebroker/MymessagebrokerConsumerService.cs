using BookingService.Brokers.Infrastructure;
using BookingService.Brokers.Mymessagebroker;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Brokers.Mymessagebroker;

public class MymessagebrokerConsumerService
    : KafkaConsumerService<MymessagebrokerMessageHandlersController>
{
    public MymessagebrokerConsumerService(
        IServiceScopeFactory serviceScopeFactory,
        KafkaOptions kafkaOptions
    )
        : base(serviceScopeFactory, kafkaOptions) { }
}

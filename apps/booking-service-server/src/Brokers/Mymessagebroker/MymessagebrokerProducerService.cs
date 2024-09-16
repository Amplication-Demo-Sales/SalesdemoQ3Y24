using BookingService.Brokers.Infrastructure;

namespace BookingService.Brokers.Mymessagebroker;

public class MymessagebrokerProducerService : InternalProducer
{
    public MymessagebrokerProducerService(string bootstrapServers)
        : base(bootstrapServers) { }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProjectBillingProcessing.Charge.EventBus.EventBus.Events
{
    public record IntegrationEvent
    {
        public IntegrationEvent()
        {
            IntegrationEventId = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid integrationEventId, DateTime createDate)
        {
            IntegrationEventId = integrationEventId;
            CreationDate = createDate;
        }

        [JsonInclude]
        public Guid IntegrationEventId { get; private init; }

        [JsonInclude]
        public DateTime CreationDate { get; private init; }
    }
}

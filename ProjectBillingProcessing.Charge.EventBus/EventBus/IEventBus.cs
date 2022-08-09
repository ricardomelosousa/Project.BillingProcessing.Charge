using ProjectBillingProcessing.Charge.EventBus.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBillingProcessing.Charge.EventBus.EventBus
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);
    }
}

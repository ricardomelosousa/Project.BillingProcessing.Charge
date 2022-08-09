using ProjectBillingProcessing.Charge.EventBus.EventBus.Events;

namespace Project.BillingProcessing.Charge.Api.Application.Dto
{
    public record ChargeIntegrationEvent : IntegrationEvent
    {
        public string Id { get; set; }
        public DateTime DueDate { get; set; }
        public string Month { get; set; }
        public decimal ChargeValue { get; set; }
        public long Identification { get; set; }
    }
}

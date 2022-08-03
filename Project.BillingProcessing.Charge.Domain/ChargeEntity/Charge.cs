using Project.BillingProcessing.Charge.Domain.SeedWork;

namespace Project.BillingProcessing.Charge.Domain.ChargeEntity
{
    public class Charge : Document
    {
        public DateTime DueDate { get; set; }
        public string Month { get; set; }
        public decimal ChargeValue { get; set; }
        public int Identification { get; set; }


    }
}

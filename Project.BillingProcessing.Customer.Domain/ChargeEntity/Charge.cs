

namespace Project.BillingProcessing.Charge.Domain.ChargeEntity
{
    public class Charge : Entity
    {
        public DateTime DueDate { get; set; }
        public decimal ChargeValue { get; set; }     
        public int Identification { get; set; }
        public Charge(DateTime dueDate, decimal chargeValue, int identification)
        {
           
        }
   

    }
}

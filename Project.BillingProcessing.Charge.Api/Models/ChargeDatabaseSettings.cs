namespace Project.BillingProcessing.Charge.Api.Models
{
    public class ChargeDatabaseSettings
    {

        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ChargesCollectionName { get; set; } = null!;
    }
}

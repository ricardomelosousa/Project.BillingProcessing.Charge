namespace Project.BillingProcessing.Charge.Infra;
public class DataService : IDataService
{
    private readonly MongoContext _mongoContext;
    public DataService(MongoContext mongoContext)
    {
        _mongoContext = mongoContext;
    }
    public IChargeRepository Charges => new ChargeRepository(_mongoContext.Database);
}




using Project.BillingProcessing.Charge.Domain.SeedWork;

namespace Project.BillingProcessing.Charge.Domain.ChargeEntity
{
    public interface IChargeRepository : IMongoRepository<Charge>
    {
        Charge CreateCharge(Charge customer);
        IList<Charge> FindBy(Expression<Func<Charge, bool>> where);
    }
}


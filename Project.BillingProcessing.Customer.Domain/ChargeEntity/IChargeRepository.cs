

namespace Project.BillingProcessing.Charge.Domain.ChargeEntity
{
    public interface IChargeRepository : IRepository<Charge>
    {
        Charge CreateCharge(Charge customer);
        IList<Charge> FindBy(Expression<Func<Charge, bool>> where);
    }
}


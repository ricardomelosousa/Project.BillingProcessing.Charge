using Project.BillingProcessing.Charge.Domain.ChargeEntity;
using Project.BillingProcessing.Charge.Domain.Service.Interface;

namespace Project.BillingProcessing.Charge.Domain.Service
{
    public class ChargeService : IChargeService
    {
        private readonly IChargeRepository _chargeRepository;
        public ChargeService(IChargeRepository chargeRepository)
        {
            _chargeRepository = chargeRepository;
        }
        public IEnumerable<ChargeEntity.Charge> FilterBy(Expression<Func<ChargeEntity.Charge, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public Task<ChargeEntity.Charge> FindByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ChargeEntity.Charge> FindOneAsync(Expression<Func<ChargeEntity.Charge, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public Task InsertOneAsync(ChargeEntity.Charge document)
        {
            throw new NotImplementedException();
        }
    }
}

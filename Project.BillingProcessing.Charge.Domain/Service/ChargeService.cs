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
            return _chargeRepository.FilterBy(filterExpression);
        }

        public async Task<ChargeEntity.Charge> FindByIdAsync(string id)
        {
            return await _chargeRepository.FindByIdAsync(id);
        }

        public async Task<ChargeEntity.Charge> FindOneAsync(Expression<Func<ChargeEntity.Charge, bool>> filterExpression)
        {
            return await _chargeRepository.FindOneAsync(filterExpression);
        }

        public async Task InsertOneAsync(ChargeEntity.Charge document)
        {
            await _chargeRepository.InsertOneAsync(document);
        }
    }
}

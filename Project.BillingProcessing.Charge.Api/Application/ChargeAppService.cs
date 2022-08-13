using AutoMapper;
using System.Linq.Expressions;

namespace Project.BillingProcessing.Charge.Api.Application.Service
{
    public class ChargeAppService : IChargeAppService
    {
        private readonly IChargeService _chargeService;

       

        public ChargeAppService(IChargeService chargeService)
        {
            _chargeService = chargeService;
          
         
        }
        public Task<Domain.ChargeEntity.Charge> FindByIdAsync(string id)
        {
            return _chargeService.FindByIdAsync(id);
        }

        public Task<Domain.ChargeEntity.Charge> FindOneAsync(Expression<Func<Domain.ChargeEntity.Charge, bool>> filterExpression)
        {
            return _chargeService.FindOneAsync(filterExpression);
        }

     
        public Task InsertOneAsync(Domain.ChargeEntity.Charge charge)
        {
            var insert = _chargeService.InsertOneAsync(charge);
            return insert;

        }

       
    }
}

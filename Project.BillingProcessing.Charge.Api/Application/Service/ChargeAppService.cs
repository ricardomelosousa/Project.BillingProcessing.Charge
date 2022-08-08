using Project.BillingProcessing.Charge.Api.Application.Interface;
using Project.BillingProcessing.Charge.Domain.Service.Interface;
using System.Linq.Expressions;

namespace Project.BillingProcessing.Charge.Api.Application.Service
{
    public class ChargeAppService : IChargeAppService
    {
        private readonly IChargeService _chargeService;
        private readonly CustomersGrpcService _customersGrpcService;

        public ChargeAppService(IChargeService chargeService, CustomersGrpcService customersGrpcService)
        {
            _chargeService = chargeService;
            _customersGrpcService = customersGrpcService;
        }


        public Task<Domain.ChargeEntity.Charge> FindByIdAsync(string id)
        {
            return _chargeService.FindByIdAsync(id);
        }

        public Task<Domain.ChargeEntity.Charge> FindOneAsync(Expression<Func<Domain.ChargeEntity.Charge, bool>> filterExpression)
        {
            return _chargeService.FindOneAsync(filterExpression);
        }

        public IEnumerable<Domain.ChargeEntity.Charge> GetByParameter(string identification, DateTime? dueDate)
        {          
           
                if (!string.IsNullOrEmpty(identification) && dueDate != null)
                {
                    var identificationValid = ValidIdentification(identification);

                    return _chargeService.FilterBy(a => a.Identification == identificationValid && a.DueDate.Month == Convert.ToDateTime(dueDate).Month);
                }
                else if (!string.IsNullOrEmpty(identification))
                {
                var identificationValid = ValidIdentification(identification);
                return _chargeService.FilterBy(a => a.Identification == identificationValid);
                }
                else if (dueDate != null)
                {
                    var ident = Convert.ToInt32(identification);
                    return _chargeService.FilterBy(a => a.DueDate.Month == Convert.ToDateTime(dueDate).Month);
                }
                return null;       

        }

        public Task InsertOneAsync(Domain.ChargeEntity.Charge charge)
        {

            return _chargeService.InsertOneAsync(charge);
        }

        private long ValidIdentification(string identification) 
        {
            try
            {
                var customer = _customersGrpcService.GetCustomerValid(identification).GetAwaiter().GetResult();
                return customer.Indentification;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);            
            }
        } 
    }
}

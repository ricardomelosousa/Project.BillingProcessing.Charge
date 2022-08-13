using System.Linq.Expressions;


namespace Project.BillingProcessing.Charge.Api.Application.Interface
{
    public interface IChargeAppService
    {
       
        Task<Domain.ChargeEntity.Charge> FindOneAsync(Expression<Func<Domain.ChargeEntity.Charge, bool>> filterExpression);

        Task<Domain.ChargeEntity.Charge> FindByIdAsync(string id);

        Task InsertOneAsync(Domain.ChargeEntity.Charge charge);
    }
}

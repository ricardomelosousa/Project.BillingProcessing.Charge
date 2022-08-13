using AutoMapper;
using GrpcChargeApi;

namespace Project.BillingProcessing.Charge.Api.Map
{
    public class CurrencyFormatter : IValueConverter<float, decimal>
    {
        public decimal Convert(float sourceMember, ResolutionContext context)
        => (decimal)sourceMember;
    }
    public class ChargeProfile : Profile
    {
        public ChargeProfile()
        {
            CreateMap<Charge.Domain.ChargeEntity.Charge, ChargeModel>();
            CreateMap<ChargeModel, Charge.Domain.ChargeEntity.Charge>()
                .ForMember(d => d.ChargeValue, opt => opt.ConvertUsing(new CurrencyFormatter(), src => src.ChargeValue));
           
        }
    }
}

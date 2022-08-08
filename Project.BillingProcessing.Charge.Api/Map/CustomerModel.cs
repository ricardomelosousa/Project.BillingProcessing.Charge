using AutoMapper;
using GrpcChargeApi;
using Project.BillingProcessing.Charge.Api.Models;
using Project.BillingProcessing.Customer.Api.Photos;

namespace Project.BillingProcessing.Charge.Api.Map
{
    public class ChargeProfile : Profile
    {
        public ChargeProfile()
        {
           CreateMap<Charge.Domain.ChargeEntity.Charge, ChargeMode>().ReverseMap();
            CreateMap<CustomerModelResponse, CustomerModel>();
        }
    }
}

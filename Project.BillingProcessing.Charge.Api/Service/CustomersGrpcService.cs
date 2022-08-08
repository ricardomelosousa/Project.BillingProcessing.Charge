using AutoMapper;
using Project.BillingProcessing.Charge.Api.Models;
using Project.BillingProcessing.Customer.Api.Photos;

namespace Project.BillingProcessing.Charge.Api.Service
{
    public class CustomersGrpcService
    {
        private readonly CustomerProtoService.CustomerProtoServiceClient _customerProtoServiceClient;
        private readonly IMapper _mapper;

        public CustomersGrpcService(CustomerProtoService.CustomerProtoServiceClient customerProtoServiceClient, IMapper mapper)
        {
            _customerProtoServiceClient = customerProtoServiceClient;
            _mapper = mapper;
        }

        public async Task<CustomerModel> GetCustomerValid(string identification)
        {
            var request = new GetCustomerByIdentificationRequest { Identification = identification };
            var response = await _customerProtoServiceClient.GetCustomerByIdentificationAsync(request);
            return _mapper.Map<CustomerModel>(response);
        }
    }
}

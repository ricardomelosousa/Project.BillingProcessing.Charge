using AutoMapper;
using Grpc.Core;
using GrpcChargeApi;

namespace Project.BillingProcessing.Charge.Api.Grpc
{
    public class ChargeGrpcService : ChargeProtoService.ChargeProtoServiceBase
    {
        private readonly IChargeService _customerService;
        private readonly ILogger<ChargeGrpcService> _logger;
        private readonly IMapper _mapper;

        public ChargeGrpcService(IChargeService customerService, ILogger<ChargeGrpcService> logger, IMapper mapper)
        {
            _customerService = customerService;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CreateChargeResponse> CreateCharge(ChargeMode request, ServerCallContext context)
        {
            await _customerService.InsertOneAsync(_mapper.Map<Domain.ChargeEntity.Charge>(request));
            return new CreateChargeResponse { Success = true };
        }

        public override async Task<ChargeMode> GetChargeByParameter(GetChargeByParameterRequest request, ServerCallContext context)
        {

            var result = _customerService.FilterBy(a => a.Identification == Convert.ToInt32(request.Identification.Replace(".", "").Replace("-", "")));
            return _mapper.Map<ChargeMode>(result);
        }
    }
}

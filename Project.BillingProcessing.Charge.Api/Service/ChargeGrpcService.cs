using AutoMapper;
using Grpc.Core;
using GrpcChargeApi;

namespace Project.BillingProcessing.Charge.Api.Grpc
{
    public class ChargeGrpcService : ChargeProtoService.ChargeProtoServiceBase
    {
        private readonly IChargeService _chargeService;
        private readonly ILogger<ChargeGrpcService> _logger;
        private readonly IMapper _mapper;

        public ChargeGrpcService(IChargeService chargeService, ILogger<ChargeGrpcService> logger, IMapper mapper)
        {
            _chargeService =  chargeService;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CreateChargeResponse> CreateCharge(ChargeModel request, ServerCallContext context)
        {
            await _chargeService.InsertOneAsync(_mapper.Map<Domain.ChargeEntity.Charge>(request));
            return new CreateChargeResponse { Success = true };
        }

        public override async Task<ChargeModel> GetChargeByParameter(GetChargeByParameterRequest request, ServerCallContext context)
        {

            var result = _chargeService.FilterBy(a => a.Identification == Convert.ToInt32(request.Identification.Replace(".", "").Replace("-", "")));
            return _mapper.Map<ChargeModel>(result);
        }
    }
}

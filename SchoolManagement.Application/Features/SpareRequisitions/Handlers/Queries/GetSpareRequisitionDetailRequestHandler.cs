using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.SpareRequisition;
using SchoolManagement.Application.Features.SpareRequisitions.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.SpareRequisitions.Handlers.Queries
{
    public class GetSpareRequisitionDetailRequestHandler : IRequestHandler<GetSpareRequisitionDetailRequest, SpareRequisitionDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.SpareRequisition> _SpareRequisitionRepository;
        public GetSpareRequisitionDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.SpareRequisition> SpareRequisitionRepository, IMapper mapper)
        {
            _SpareRequisitionRepository = SpareRequisitionRepository;
            _mapper = mapper;
        }
        public async Task<SpareRequisitionDto> Handle(GetSpareRequisitionDetailRequest request, CancellationToken cancellationToken)
        {
            var SpareRequisition = await _SpareRequisitionRepository.Get(request.SpareRequisitionId);
            return _mapper.Map<SpareRequisitionDto>(SpareRequisition);
        }
    }
}

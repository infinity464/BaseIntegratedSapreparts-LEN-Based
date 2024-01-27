using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.CorrectiveMaintenence;
using SchoolManagement.Application.Features.CorrectiveMaintenences.Requests.Queries;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.CorrectiveMaintenences.Handlers.Queries
{
    public class GetCorrectiveMaintenenceDetailRequestHandler : IRequestHandler<GetCorrectiveMaintenenceDetailRequest, CorrectiveMaintenenceDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<CorrectiveMaintenence> _CorrectiveMaintenenceRepository;
        public GetCorrectiveMaintenenceDetailRequestHandler(ISchoolManagementRepository<CorrectiveMaintenence> CorrectiveMaintenenceRepository, IMapper mapper)
        {
            _CorrectiveMaintenenceRepository = CorrectiveMaintenenceRepository;
            _mapper = mapper;
        }
        public async Task<CorrectiveMaintenenceDto> Handle(GetCorrectiveMaintenenceDetailRequest request, CancellationToken cancellationToken)
        {
            var CorrectiveMaintenence =  _CorrectiveMaintenenceRepository.FinedOneInclude(x=>x.CorrectiveMaintenenceId == request.CorrectiveMaintenenceId, "EquipmentName", "IssueRegister", "ItemDetail");
            return _mapper.Map<CorrectiveMaintenenceDto>(CorrectiveMaintenence);
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ScheduleMaintenenceTypes;
using SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Handlers.Queries
{
    public class GetScheduleMaintenenceTypeDetailRequestHandler : IRequestHandler<GetScheduleMaintenenceTypeDetailRequest, ScheduleMaintenenceTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.ScheduleMaintenenceType> _ScheduleMaintenenceTypeRepository;
        public GetScheduleMaintenenceTypeDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.ScheduleMaintenenceType> ScheduleMaintenenceTypeRepository, IMapper mapper)
        {
            _ScheduleMaintenenceTypeRepository = ScheduleMaintenenceTypeRepository;
            _mapper = mapper;
        }
        public async Task<ScheduleMaintenenceTypeDto> Handle(GetScheduleMaintenenceTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var ScheduleMaintenenceType = await _ScheduleMaintenenceTypeRepository.Get(request.ScheduleMaintenenceTypeId);
            return _mapper.Map<ScheduleMaintenenceTypeDto>(ScheduleMaintenenceType);
        }
    }
}

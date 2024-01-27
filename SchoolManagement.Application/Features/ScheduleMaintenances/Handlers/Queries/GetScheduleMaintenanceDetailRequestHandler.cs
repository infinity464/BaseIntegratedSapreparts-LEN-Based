using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ScheduleMaintenances;
using SchoolManagement.Application.Features.ScheduleMaintenances.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ScheduleMaintenances.Handlers.Queries
{
    public class GetScheduleMaintenanceDetailRequestHandler : IRequestHandler<GetScheduleMaintenanceDetailRequest, ScheduleMaintenanceDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.ScheduleMaintenance> _ScheduleMaintenanceRepository;
        public GetScheduleMaintenanceDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.ScheduleMaintenance> ScheduleMaintenanceRepository, IMapper mapper)
        {
            _ScheduleMaintenanceRepository = ScheduleMaintenanceRepository;
            _mapper = mapper;
        }
        public async Task<ScheduleMaintenanceDto> Handle(GetScheduleMaintenanceDetailRequest request, CancellationToken cancellationToken)
        {
            var ScheduleMaintenance = await _ScheduleMaintenanceRepository.Get(request.ScheduleMaintenanceId);
            return _mapper.Map<ScheduleMaintenanceDto>(ScheduleMaintenance);
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.PreventiveMaintenance;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Handlers.Queries
{
    public class GetPreventiveMaintenanceDetailRequestHandler : IRequestHandler<GetPreventiveMaintenanceDetailRequest, PreventiveMaintenanceDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.PreventiveMaintenance> _PreventiveMaintenanceRepository;
        public GetPreventiveMaintenanceDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.PreventiveMaintenance> PreventiveMaintenanceRepository, IMapper mapper)
        {
            _PreventiveMaintenanceRepository = PreventiveMaintenanceRepository;
            _mapper = mapper;
        }
        public async Task<PreventiveMaintenanceDto> Handle(GetPreventiveMaintenanceDetailRequest request, CancellationToken cancellationToken)
        {
            //var PreventiveMaintenance = await _PreventiveMaintenanceRepository.Get(request.PreventiveMaintenanceId);
            var PreventiveMaintenance = _PreventiveMaintenanceRepository.FinedOneInclude(x => x.PreventiveMaintenanceId == request.PreventiveMaintenanceId, "ItemDetail", "MaintenanceSubCategory", "MaintenanceCategory");
            return _mapper.Map<PreventiveMaintenanceDto>(PreventiveMaintenance);
        }
    }
}

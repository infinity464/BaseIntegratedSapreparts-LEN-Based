using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.PreventiveMaintenance;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Handlers.Queries
{
    public class GetPreventiveListByDepartmentNameIdRequestHandler : IRequestHandler<GetPreventiveListByDepartmentNameIdRequest, List<PreventiveMaintenanceDto>>
    {
        private readonly ISchoolManagementRepository<PreventiveMaintenance> _PreventiveMaintenanceRepository;

        private readonly IMapper _mapper;
        public GetPreventiveListByDepartmentNameIdRequestHandler(ISchoolManagementRepository<PreventiveMaintenance> PreventiveMaintenanceRepository, IMapper mapper)
        {
            _PreventiveMaintenanceRepository = PreventiveMaintenanceRepository;
            _mapper = mapper;
        }

        public async Task<List<PreventiveMaintenanceDto>> Handle(GetPreventiveListByDepartmentNameIdRequest request, CancellationToken cancellationToken)
        {
            IQueryable<PreventiveMaintenance> PreventiveMaintenances = _PreventiveMaintenanceRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId ==  request.DepartmentNameId, "ItemDetail", "MaintenanceCategory", "MaintenanceType", "JobDescriptionEntryPms");
            var totalCount = PreventiveMaintenances.Count();
            PreventiveMaintenances = PreventiveMaintenances.OrderByDescending(x => x.PreventiveMaintenanceId);
            var PreventiveMaintenanceDtos = _mapper.Map<List<PreventiveMaintenanceDto>>(PreventiveMaintenances);

            return PreventiveMaintenanceDtos;
        }

    }
}

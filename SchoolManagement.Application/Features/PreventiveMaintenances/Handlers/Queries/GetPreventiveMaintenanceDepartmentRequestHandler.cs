using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.PreventiveMaintenance;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Handlers.Queries
{
    public class GetPreventiveMaintenanceDepartmentRequestHandler : IRequestHandler<GetPreventiveMaintenanceDepartmentRequest, List<PreventiveMaintenanceDto>>
    {
        private readonly ISchoolManagementRepository<PreventiveMaintenance> _PreventiveMaintenanceRepository;

        private readonly IMapper _mapper;
        public GetPreventiveMaintenanceDepartmentRequestHandler(ISchoolManagementRepository<PreventiveMaintenance> PreventiveMaintenanceRepository, IMapper mapper)
        {
            _PreventiveMaintenanceRepository = PreventiveMaintenanceRepository;
            _mapper = mapper;
        }

        public async Task<List<PreventiveMaintenanceDto>> Handle(GetPreventiveMaintenanceDepartmentRequest request, CancellationToken cancellationToken)
        {
            IQueryable<PreventiveMaintenance> PreventiveMaintenances = _PreventiveMaintenanceRepository.FilterWithInclude(x => x.DepartmentNameId == request.DepartmentNameId, "MaintenanceCategory", "MaintenanceSubCategory", "ItemDetail");

            var PreventiveMaintenanceDtos = _mapper.Map<List<PreventiveMaintenanceDto>>(PreventiveMaintenances);

            return PreventiveMaintenanceDtos;
        }

    }
}

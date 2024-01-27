using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.PreventiveMaintenance;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Handlers.Queries
{
    public class GetPreventiveMaintenanceListByParameterRequestHandler :  IRequestHandler<GetPreventiveMaintenanceListByParameterRequest, List<PreventiveMaintenanceDto>>
    {
        private readonly ISchoolManagementRepository<PreventiveMaintenance> _PreventiveMaintenanceRepository;
        private readonly IMapper _mapper;


        public GetPreventiveMaintenanceListByParameterRequestHandler(ISchoolManagementRepository<PreventiveMaintenance> PreventiveMaintenanceRepository, IMapper mapper)
        {
            _PreventiveMaintenanceRepository = PreventiveMaintenanceRepository;
            _mapper = mapper;
        }

        

        public async Task<List<PreventiveMaintenanceDto>> Handle(GetPreventiveMaintenanceListByParameterRequest request, CancellationToken cancellationToken)
        {
            IQueryable<PreventiveMaintenance> PreventiveMaintenances = _PreventiveMaintenanceRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.MaintenanceTypeId == request.MaintenanceTypeId  && x.MaintenanceCategoryId == request.MaintenanceCategoryId && x.ItemDetailId ==request.ItemDetailId, "JobDescriptionEntryPms", "JobDescriptionEntryPms.ByWhom");
            

            var PreventiveMaintenanceDtos = _mapper.Map<List<PreventiveMaintenanceDto>>(PreventiveMaintenances);

            return PreventiveMaintenanceDtos;
        }
    }
}

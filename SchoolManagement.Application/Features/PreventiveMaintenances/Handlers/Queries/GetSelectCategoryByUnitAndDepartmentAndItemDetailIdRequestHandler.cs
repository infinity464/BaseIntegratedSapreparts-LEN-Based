using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Handlers.Queries
{
    public class GetSelectCategoryByUnitAndDepartmentAndItemDetailIdRequestHandler : IRequestHandler<GetSelectCategoryByUnitAndDepartmentAndItemDetailIdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<PreventiveMaintenance> _PreventiveMaintenanceRepository;


        public GetSelectCategoryByUnitAndDepartmentAndItemDetailIdRequestHandler(ISchoolManagementRepository<PreventiveMaintenance> PreventiveMaintenanceRepository)
        {
            _PreventiveMaintenanceRepository = PreventiveMaintenanceRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectCategoryByUnitAndDepartmentAndItemDetailIdRequest request, CancellationToken cancellationToken)
        {
            var PreventiveMaintenances =  _PreventiveMaintenanceRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemDetailId == request.ItemDetailId, "MaintenanceType");
            List<SelectedModel> selectModels = PreventiveMaintenances.Select(x => new SelectedModel
            {
                Text = x.MaintenanceType.Name,
                Value = x.MaintenanceTypeId
            }).ToList();
            return selectModels;
        }
    }
    
}

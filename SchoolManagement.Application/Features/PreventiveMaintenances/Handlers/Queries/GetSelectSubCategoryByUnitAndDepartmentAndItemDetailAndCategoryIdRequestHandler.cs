using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Handlers.Queries
{
    public class GetSelectSubCategoryByUnitAndDepartmentAndItemDetailAndCategoryIdRequestHandler : IRequestHandler<GetSelectSubCategoryByUnitAndDepartmentAndItemDetailAndCategoryIdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<PreventiveMaintenance> _PreventiveMaintenanceRepository;


        public GetSelectSubCategoryByUnitAndDepartmentAndItemDetailAndCategoryIdRequestHandler(ISchoolManagementRepository<PreventiveMaintenance> PreventiveMaintenanceRepository)
        {
            _PreventiveMaintenanceRepository = PreventiveMaintenanceRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectSubCategoryByUnitAndDepartmentAndItemDetailAndCategoryIdRequest request, CancellationToken cancellationToken)
        {
            var PreventiveMaintenances =  _PreventiveMaintenanceRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemDetailId == request.ItemDetailId && x.MaintenanceTypeId == request.MaintenanceTypeId, "MaintenanceCategory");
            List<SelectedModel> selectModels = PreventiveMaintenances.Select(x => new SelectedModel
            {
                Text = x.MaintenanceCategory.CategoryName,
                Value = x.MaintenanceCategoryId
            }).ToList();
            return selectModels;
        }
    }
    
}

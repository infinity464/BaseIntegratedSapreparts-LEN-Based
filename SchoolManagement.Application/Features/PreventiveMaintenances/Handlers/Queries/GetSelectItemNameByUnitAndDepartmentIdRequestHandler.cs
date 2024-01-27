using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Handlers.Queries
{
    public class GetSelectItemNameByUnitAndDepartmentIdRequestHandler : IRequestHandler<GetSelectItemNameByUnitAndDepartmentIdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<PreventiveMaintenance> _PreventiveMaintenanceRepository;


        public GetSelectItemNameByUnitAndDepartmentIdRequestHandler(ISchoolManagementRepository<PreventiveMaintenance> PreventiveMaintenanceRepository)
        {
            _PreventiveMaintenanceRepository = PreventiveMaintenanceRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectItemNameByUnitAndDepartmentIdRequest request, CancellationToken cancellationToken)
        {
            var PreventiveMaintenances =  _PreventiveMaintenanceRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId, "ItemDetail");
            List<SelectedModel> selectModels = PreventiveMaintenances.Select(x => new SelectedModel
            {
                Text = x.ItemDetail.NameOfItem,
                Value = x.ItemDetailId
            }).ToList();
            return selectModels;
        }
    }
    
}

using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Demands.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Demands.Handlers.Queries
{
    public class GetEquipmentByUnitAndDepartmentAndCategoryIdRequestHandler : IRequestHandler<GetEquipmenteByUnitAndDepartmentAndCategoryIdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Demand> _DemandRepository;


        public GetEquipmentByUnitAndDepartmentAndCategoryIdRequestHandler(ISchoolManagementRepository<Demand> DemandRepository)
        {
            _DemandRepository = DemandRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetEquipmenteByUnitAndDepartmentAndCategoryIdRequest request, CancellationToken cancellationToken)
        {
            var Demands =  _DemandRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId, "EquipmentName");
            List<SelectedModel> selectModels = Demands.Select(x => new SelectedModel
            {
                Text = x.EquipmentName.Name,
                Value = x.EquipmentNameId
            }).ToList();
            return selectModels;
        }
    }
    
}

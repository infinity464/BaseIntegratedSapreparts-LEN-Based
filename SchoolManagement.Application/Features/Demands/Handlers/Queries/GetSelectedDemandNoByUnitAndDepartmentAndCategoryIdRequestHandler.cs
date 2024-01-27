using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Demands.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Demands.Handlers.Queries
{
    public class GetSelectedDemandNoByUnitAndDepartmentAndCategoryIdRequestHandler : IRequestHandler<GetSelectedDemandNoByUnitAndDepartmentAndCategoryIdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Demand> _DemandRepository;


        public GetSelectedDemandNoByUnitAndDepartmentAndCategoryIdRequestHandler(ISchoolManagementRepository<Demand> DemandRepository)
        {
            _DemandRepository = DemandRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDemandNoByUnitAndDepartmentAndCategoryIdRequest request, CancellationToken cancellationToken)
        {
            var Demands =  _DemandRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId && x.EquipmentNameId == request.EquipmentNameId, "ItemDetail");
            List<SelectedModel> selectModels = Demands.Select(x => new SelectedModel
            {
                Text = x.DemandNo,
                Value = x.DemandId
            }).ToList();
            return selectModels;
        }
    }
    
}

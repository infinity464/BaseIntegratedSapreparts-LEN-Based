using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Demands.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Demands.Handlers.Queries
{
    public class GetNameofItemByUnitAndDepartmentAndCategoryIdRequestHandler : IRequestHandler<GetNameofItemByUnitAndDepartmentAndCategoryIdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Demand> _DemandRepository;


        public GetNameofItemByUnitAndDepartmentAndCategoryIdRequestHandler(ISchoolManagementRepository<Demand> DemandRepository)
        {
            _DemandRepository = DemandRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetNameofItemByUnitAndDepartmentAndCategoryIdRequest request, CancellationToken cancellationToken)
        {
            var Demands =  _DemandRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId && x.EquipmentNameId == request.EquipmentNameId, "ItemDetail");
            List<SelectedModel> selectModels = Demands.Select(x => new SelectedModel
            {
                Text = x.ItemDetail.NameOfItem,
                Value = x.ItemDetailId
            }).ToList();
            return selectModels;
        }
    }
    
}

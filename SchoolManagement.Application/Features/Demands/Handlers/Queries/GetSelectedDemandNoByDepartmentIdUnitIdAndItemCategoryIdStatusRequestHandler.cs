using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Demands.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Demands.Handlers.Queries
{
    public class GetSelectedDemandNoByDepartmentIdUnitIdAndItemCategoryIdStatusRequestHandler : IRequestHandler<GetSelectedDemandNoByDepartmentIdUnitIdAndItemCategoryIdStatusRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Demand> _DemandRepository;


        public GetSelectedDemandNoByDepartmentIdUnitIdAndItemCategoryIdStatusRequestHandler(ISchoolManagementRepository<Demand> DemandRepository)
        {
            _DemandRepository = DemandRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDemandNoByDepartmentIdUnitIdAndItemCategoryIdStatusRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Demand> Demands = _DemandRepository.FilterWithInclude((x => x.IsActive && x.UnitId == request.UnitId && x.DepartmentNameId==request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId && x.StoreStatus ==request.StoreStatus && x.SftStatus == request.SftStatus), "ItemDetail");
            List<SelectedModel> selectModels = Demands.Select(x => new SelectedModel 
            {
                Text = x.DemandNo,
                Value = x.DemandId
            }).ToList();
            return selectModels;
        }
    }
}

using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Demands.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Demands.Handlers.Queries
{
    public class GetSelectedDemandQtyFromDemandByParameterRequestHandler : IRequestHandler<GetSelectedDemandQtyFromDemandByParameterRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Demand> _DemandRepository;


        public GetSelectedDemandQtyFromDemandByParameterRequestHandler(ISchoolManagementRepository<Demand> DemandRepository)
        {
            _DemandRepository = DemandRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDemandQtyFromDemandByParameterRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Demand> Demands = _DemandRepository.FilterWithInclude((x => x.IsActive && x.UnitId == request.UnitId && x.DepartmentNameId==request.DepartmentNameId && x.ItemCategoryId==request.ItemCategoryId && x.DemandId == request.DemandId), "ItemDetail");
            List<SelectedModel> selectModels = Demands.Select(x => new SelectedModel 
            {
                Text = x.DemandQty,
                Value = x.DemandId
            }).ToList();
            return selectModels;
        }
    }
}

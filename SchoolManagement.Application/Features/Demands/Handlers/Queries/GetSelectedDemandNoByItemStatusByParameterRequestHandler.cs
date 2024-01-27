using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Demands.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Application.Enum;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Demands.Handlers.Queries
{
    public class GetSelectedDemandNoByItemStatusByParameterRequestHandler : IRequestHandler<GetSelectedDemandNoByItemStatusByParameterRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Demand> _DemandRepository;


        public GetSelectedDemandNoByItemStatusByParameterRequestHandler(ISchoolManagementRepository<Demand> DemandRepository)
        {
            _DemandRepository = DemandRepository;
        }
         
        public async Task<List<SelectedModel>> Handle(GetSelectedDemandNoByItemStatusByParameterRequest request, CancellationToken cancellationToken)
        {
            if(request.ItemStatusId == (int)(SchoolManagement.Application.Enum.ItemStatus.ProcurementStatus))
            {
                IQueryable<Demand> Demands = _DemandRepository.FilterWithInclude((x => x.IsActive && x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId && x.ItemStatusId == request.ItemStatusId && x.ProcurementStatus == request.Status), "ItemDetail");
                List<SelectedModel> selectModels = Demands.Select(x => new SelectedModel
                {
                    Text = x.DemandNo,
                    Value = x.DemandId
                }).ToList();
                return selectModels;
            }
            else if (request.ItemStatusId == (int)(SchoolManagement.Application.Enum.ItemStatus.SftStatus))
            {
                IQueryable<Demand> Demands = _DemandRepository.FilterWithInclude((x => x.IsActive && x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId && x.ItemStatusId == request.ItemStatusId && x.SftStatus == request.Status), "ItemDetail");
                List<SelectedModel> selectModels = Demands.Select(x => new SelectedModel
                {
                    Text = x.DemandNo,
                    Value = x.DemandId
                }).ToList();
                return selectModels;
            }
            else if (request.ItemStatusId == (int)(SchoolManagement.Application.Enum.ItemStatus.StoreStatus))
            {
                IQueryable<Demand> Demands = _DemandRepository.FilterWithInclude((x => x.IsActive && x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemCategoryId == request.ItemCategoryId && x.ItemStatusId == request.ItemStatusId && x.StoreStatus == request.Status), "ItemDetail");
                List<SelectedModel> selectModels = Demands.Select(x => new SelectedModel
                {
                    Text = x.DemandNo,
                    Value = x.DemandId
                }).ToList();
                return selectModels;
            }
            else
            {
                return null;
            }
            
        }
    }
}

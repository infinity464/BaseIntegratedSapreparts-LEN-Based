using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Demands.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;
 
namespace SchoolManagement.Application.Features.Demands.Handlers.Queries
{
    public class GetSelectedPartNoFromDemandByItemStatusAndProcureStatusRequestHandler : IRequestHandler<GetSelectedPartNoFromDemandByItemStatusAndProcureStatusRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Demand> _DemandRepository;


        public GetSelectedPartNoFromDemandByItemStatusAndProcureStatusRequestHandler(ISchoolManagementRepository<Demand> DemandRepository)
        {
            _DemandRepository = DemandRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedPartNoFromDemandByItemStatusAndProcureStatusRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Demand> Demands = _DemandRepository.FilterWithInclude((x => x.IsActive && x.ItemStatusId==request.ItemStatusId && x.ProcurementStatus == request.ProcurementStatus), "ItemDetail");
            List<SelectedModel> selectModels = Demands.Select(x => new SelectedModel 
            {
                Text = x.ItemDetail.PartNo +" - "+ x.ItemDetail.NameOfItem,
                Value = x.DemandId
            }).ToList();
            return selectModels;
        }
    }
}

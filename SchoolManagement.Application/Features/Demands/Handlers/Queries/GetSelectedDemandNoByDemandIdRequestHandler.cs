using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Demands.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Demands.Handlers.Queries
{
    public class GetSelectedDemandNoByDemandIdRequestHandler : IRequestHandler<GetSelectedDemandNoByDemandIdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Demand> _DemandRepository;


        public GetSelectedDemandNoByDemandIdRequestHandler(ISchoolManagementRepository<Demand> DemandRepository)
        {
            _DemandRepository = DemandRepository;
        }
         
        public async Task<List<SelectedModel>> Handle(GetSelectedDemandNoByDemandIdRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Demand> Demands = _DemandRepository.FilterWithInclude((x => x.IsActive && x.DemandId == request.DemandId), "ItemDetail");
            List<SelectedModel> selectModels = Demands.Select(x => new SelectedModel
            {
                Text = x.DemandNo,
                Value = x.DemandId
            }).ToList();
            return selectModels;
        }
    }
}

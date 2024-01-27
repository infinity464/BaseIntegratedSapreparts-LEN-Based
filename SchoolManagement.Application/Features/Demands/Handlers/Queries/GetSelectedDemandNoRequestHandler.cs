using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Demands.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Demands.Handlers.Queries
{
    public class GetSelectedDemandNoRequestHandler : IRequestHandler<GetSelectedDemandNoRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Demand> _DemandRepository;


        public GetSelectedDemandNoRequestHandler(ISchoolManagementRepository<Demand> DemandRepository)
        {
            _DemandRepository = DemandRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedDemandNoRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Demand> Demands = _DemandRepository.FilterWithInclude((x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentId), "ItemDetail");
            List<SelectedModel> selectModels = Demands.Select(x => new SelectedModel 
            {
                Text = x.RefPoNo,
                Value = x.DemandId
            }).ToList();
            return selectModels;
        }
    }
}

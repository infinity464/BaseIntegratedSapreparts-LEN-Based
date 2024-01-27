using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Handlers.Queries
{
    public class GetJICByUnitAndDepartmentAndItemDetailIdRequestHandler : IRequestHandler<GetJICByUnitAndDepartmentAndItemDetailIdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<JobDescriptionEntryPms> _JobDescriptionEntryPmsRepository;


        public GetJICByUnitAndDepartmentAndItemDetailIdRequestHandler(ISchoolManagementRepository<JobDescriptionEntryPms> JobDescriptionEntryPmsRepository)
        {
            _JobDescriptionEntryPmsRepository = JobDescriptionEntryPmsRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetJICByUnitAndDepartmentAndItemDetailIdRequest request, CancellationToken cancellationToken)
        {
            var JobDescriptionEntryPmses = _JobDescriptionEntryPmsRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.ItemDetailId == request.ItemDetailId, "ItemDetail");
            List<SelectedModel> selectModels = JobDescriptionEntryPmses.Select(x => new SelectedModel
            {
                Text = x.Jic,
                Value = x.JobDescriptionEntryPmsId
            }).ToList();
            return selectModels;
        }
    }
    
}

using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Handlers.Queries
{
    public class GetSelectedJobDescriptionEntryPmsRequestHandler : IRequestHandler<GetSelectedJobDescriptionEntryPmsRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<JobDescriptionEntryPms> _JobDescriptionEntryPmsRepository;


        public GetSelectedJobDescriptionEntryPmsRequestHandler(ISchoolManagementRepository<JobDescriptionEntryPms> JobDescriptionEntryPmsRepository)
        {
            _JobDescriptionEntryPmsRepository = JobDescriptionEntryPmsRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedJobDescriptionEntryPmsRequest request, CancellationToken cancellationToken)
        {
            ICollection<JobDescriptionEntryPms> codeValues = await _JobDescriptionEntryPmsRepository.FilterAsync(x => x.SpareRequiredStatusId == 1);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.JobDescription,
                Value = x.JobDescriptionEntryPmsId
            }).ToList();
            return selectModels;
        }
    }
}

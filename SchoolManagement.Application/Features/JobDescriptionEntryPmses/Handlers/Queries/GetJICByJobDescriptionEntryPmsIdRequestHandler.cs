using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Handlers.Queries
{
    public class GetJICByJobDescriptionEntryPmsIdRequestHandler : IRequestHandler<GetJICByJobDescriptionEntryPmsIdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<JobDescriptionEntryPms> _JobDescriptionEntryPmsRepository;

          
        public GetJICByJobDescriptionEntryPmsIdRequestHandler(ISchoolManagementRepository<JobDescriptionEntryPms> JobDescriptionEntryPmsRepository)
        {
            _JobDescriptionEntryPmsRepository = JobDescriptionEntryPmsRepository;           
        }

        public async Task<List<SelectedModel>> Handle(GetJICByJobDescriptionEntryPmsIdRequest request, CancellationToken cancellationToken)
        {
            ICollection<JobDescriptionEntryPms> JobDescriptionEntryPmss = await _JobDescriptionEntryPmsRepository.FilterAsync(x =>x.JobDescriptionEntryPmsId == request.JobDescriptionEntryPmsId);
            List<SelectedModel> selectModels = JobDescriptionEntryPmss.Select(x => new SelectedModel
            {
                Text = x.Jic, 
                Value = x.JobDescriptionEntryPmsId 
            }).ToList();
            return selectModels;
        }
    }
}

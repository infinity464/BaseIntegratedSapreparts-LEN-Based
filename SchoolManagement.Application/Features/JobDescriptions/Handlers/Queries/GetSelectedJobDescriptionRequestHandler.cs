using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.JobDescriptions.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.JobDescriptions.Handlers.Queries
{
    public class GetSelectedJobDescriptionRequestHandler : IRequestHandler<GetSelectedJobDescriptionRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<JobDescription> _JobDescriptionRepository;


        public GetSelectedJobDescriptionRequestHandler(ISchoolManagementRepository<JobDescription> JobDescriptionRepository)
        {
            _JobDescriptionRepository = JobDescriptionRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedJobDescriptionRequest request, CancellationToken cancellationToken)
        {
            ICollection<JobDescription> codeValues = await _JobDescriptionRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.JobDescriptionId
            }).ToList();
            return selectModels;
        }
    }
}

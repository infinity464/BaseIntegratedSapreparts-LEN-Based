using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.StatusOfJobs.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.StatusOfJobs.Handlers.Queries
{
    public class GetSelectedStatusOfJobRequestHandler : IRequestHandler<GetSelectedStatusOfJobRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<StatusOfJob> _StatusOfJobRepository;


        public GetSelectedStatusOfJobRequestHandler(ISchoolManagementRepository<StatusOfJob> StatusOfJobRepository)
        {
            _StatusOfJobRepository = StatusOfJobRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedStatusOfJobRequest request, CancellationToken cancellationToken)
        {
            ICollection<StatusOfJob> codeValues = await _StatusOfJobRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.StatusOfJobId
            }).ToList();
            return selectModels;
        }
    }
}

using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.JobCategoryMaintenences.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.JobCategoryMaintenences.Handlers.Queries
{
    public class GetSelectedJobCategoryMaintenenceRequestHandler : IRequestHandler<GetSelectedJobCategoryMaintenenceRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<JobCategoryMaintenence> _JobCategoryMaintenenceRepository;


        public GetSelectedJobCategoryMaintenenceRequestHandler(ISchoolManagementRepository<JobCategoryMaintenence> JobCategoryMaintenenceRepository)
        {
            _JobCategoryMaintenenceRepository = JobCategoryMaintenenceRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedJobCategoryMaintenenceRequest request, CancellationToken cancellationToken)
        {
            ICollection<JobCategoryMaintenence> codeValues = await _JobCategoryMaintenenceRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.CategoryName,
                Value = x.JobCategoryMaintenenceId
            }).ToList();
            return selectModels;
        }
    }
}

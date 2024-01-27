using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.JobCategorys.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.JobCategorys.Handlers.Queries
{
    public class GetSelectedJobCategoryRequestHandler : IRequestHandler<GetSelectedJobCategoryRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<JobCategory> _JobCategoryRepository;


        public GetSelectedJobCategoryRequestHandler(ISchoolManagementRepository<JobCategory> JobCategoryRepository)
        {
            _JobCategoryRepository = JobCategoryRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedJobCategoryRequest request, CancellationToken cancellationToken)
        {
            ICollection<JobCategory> codeValues = await _JobCategoryRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.JobCategoryId
            }).ToList();
            return selectModels;
        }
    }
}

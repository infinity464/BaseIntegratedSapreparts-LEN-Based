using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.JobCategory;
using SchoolManagement.Application.Features.JobCategorys.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.JobCategorys.Handlers.Queries
{
    public class GetJobCategoryDetailRequestHandler : IRequestHandler<GetJobCategoryDetailRequest, JobCategoryDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.JobCategory> _JobCategoryRepository;
        public GetJobCategoryDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.JobCategory> JobCategoryRepository, IMapper mapper)
        {
            _JobCategoryRepository = JobCategoryRepository;
            _mapper = mapper;
        }
        public async Task<JobCategoryDto> Handle(GetJobCategoryDetailRequest request, CancellationToken cancellationToken)
        {
            var JobCategory = await _JobCategoryRepository.Get(request.JobCategoryId);
            return _mapper.Map<JobCategoryDto>(JobCategory);
        }
    }
}

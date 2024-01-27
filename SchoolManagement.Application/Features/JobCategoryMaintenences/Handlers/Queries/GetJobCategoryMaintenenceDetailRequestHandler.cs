using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.JobCategoryMaintenences;
using SchoolManagement.Application.Features.JobCategoryMaintenences.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.JobCategoryMaintenences.Handlers.Queries
{
    public class GetJobCategoryMaintenenceDetailRequestHandler : IRequestHandler<GetJobCategoryMaintenenceDetailRequest, JobCategoryMaintenenceDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.JobCategoryMaintenence> _JobCategoryMaintenenceRepository;
        public GetJobCategoryMaintenenceDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.JobCategoryMaintenence> JobCategoryMaintenenceRepository, IMapper mapper)
        {
            _JobCategoryMaintenenceRepository = JobCategoryMaintenenceRepository;
            _mapper = mapper;
        }
        public async Task<JobCategoryMaintenenceDto> Handle(GetJobCategoryMaintenenceDetailRequest request, CancellationToken cancellationToken)
        {
            var JobCategoryMaintenence = await _JobCategoryMaintenenceRepository.Get(request.JobCategoryMaintenenceId);
            return _mapper.Map<JobCategoryMaintenenceDto>(JobCategoryMaintenence);
        }
    }
}

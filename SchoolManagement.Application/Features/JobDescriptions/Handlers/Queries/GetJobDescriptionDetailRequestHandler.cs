using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.JobDescription;
using SchoolManagement.Application.Features.JobDescriptions.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.JobDescriptions.Handlers.Queries
{
    public class GetJobDescriptionDetailRequestHandler : IRequestHandler<GetJobDescriptionDetailRequest, JobDescriptionDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.JobDescription> _JobDescriptionRepository;
        public GetJobDescriptionDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.JobDescription> JobDescriptionRepository, IMapper mapper)
        {
            _JobDescriptionRepository = JobDescriptionRepository;
            _mapper = mapper;
        }
        public async Task<JobDescriptionDto> Handle(GetJobDescriptionDetailRequest request, CancellationToken cancellationToken)
        {
            var JobDescription = await _JobDescriptionRepository.Get(request.JobDescriptionId);
            return _mapper.Map<JobDescriptionDto>(JobDescription);
        }
    }
}

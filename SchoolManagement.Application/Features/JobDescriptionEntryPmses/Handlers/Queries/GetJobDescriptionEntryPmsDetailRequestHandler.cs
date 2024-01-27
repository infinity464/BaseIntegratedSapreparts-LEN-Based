using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.JobDescriptionEntryPms;
using SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmss.Handlers.Queries
{
    public class GetJobDescriptionEntryPmsDetailRequestHandler : IRequestHandler<GetJobDescriptionEntryPmsDetailRequest, JobDescriptionEntryPmsDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.JobDescriptionEntryPms> _JobDescriptionEntryPmsRepository;
        public GetJobDescriptionEntryPmsDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.JobDescriptionEntryPms> JobDescriptionEntryPmsRepository, IMapper mapper)
        {
            _JobDescriptionEntryPmsRepository = JobDescriptionEntryPmsRepository;
            _mapper = mapper;
        }
        public async Task<JobDescriptionEntryPmsDto> Handle(GetJobDescriptionEntryPmsDetailRequest request, CancellationToken cancellationToken)
        {
            var JobDescriptionEntryPms = await _JobDescriptionEntryPmsRepository.Get(request.JobDescriptionEntryPmsId);
            return _mapper.Map<JobDescriptionEntryPmsDto>(JobDescriptionEntryPms);
        }
    }
}

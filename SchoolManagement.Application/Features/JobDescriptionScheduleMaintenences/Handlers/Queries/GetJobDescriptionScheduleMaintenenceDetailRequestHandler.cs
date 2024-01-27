using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.JobDescriptionScheduleMaintenences;
using SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Handlers.Queries
{
    public class GetJobDescriptionScheduleMaintenenceDetailRequestHandler : IRequestHandler<GetJobDescriptionScheduleMaintenenceDetailRequest, JobDescriptionScheduleMaintenenceDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.JobDescriptionScheduleMaintenence> _JobDescriptionScheduleMaintenenceRepository;
        public GetJobDescriptionScheduleMaintenenceDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.JobDescriptionScheduleMaintenence> JobDescriptionScheduleMaintenenceRepository, IMapper mapper)
        {
            _JobDescriptionScheduleMaintenenceRepository = JobDescriptionScheduleMaintenenceRepository;
            _mapper = mapper;
        }
        public async Task<JobDescriptionScheduleMaintenenceDto> Handle(GetJobDescriptionScheduleMaintenenceDetailRequest request, CancellationToken cancellationToken)
        {
            var JobDescriptionScheduleMaintenence = await _JobDescriptionScheduleMaintenenceRepository.Get(request.JobDescriptionScheduleMaintenenceId);
            return _mapper.Map<JobDescriptionScheduleMaintenenceDto>(JobDescriptionScheduleMaintenence);
        }
    }
}

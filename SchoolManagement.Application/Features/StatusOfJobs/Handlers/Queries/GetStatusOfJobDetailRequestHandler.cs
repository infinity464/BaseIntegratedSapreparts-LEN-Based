using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.StatusOfJob;
using SchoolManagement.Application.Features.StatusOfJobs.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.StatusOfJobs.Handlers.Queries
{
    public class GetStatusOfJobDetailRequestHandler : IRequestHandler<GetStatusOfJobDetailRequest, StatusOfJobDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.StatusOfJob> _StatusOfJobRepository;
        public GetStatusOfJobDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.StatusOfJob> StatusOfJobRepository, IMapper mapper)
        {
            _StatusOfJobRepository = StatusOfJobRepository;
            _mapper = mapper;
        }
        public async Task<StatusOfJobDto> Handle(GetStatusOfJobDetailRequest request, CancellationToken cancellationToken)
        {
            var StatusOfJob = await _StatusOfJobRepository.Get(request.StatusOfJobId);
            return _mapper.Map<StatusOfJobDto>(StatusOfJob);
        }
    }
}

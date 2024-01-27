using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Priority;
using SchoolManagement.Application.Features.Prioritys.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Prioritys.Handlers.Queries
{
    public class GetPriorityDetailRequestHandler : IRequestHandler<GetPriorityDetailRequest, PriorityDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.Priority> _PriorityRepository;
        public GetPriorityDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.Priority> PriorityRepository, IMapper mapper)
        {
            _PriorityRepository = PriorityRepository;
            _mapper = mapper;
        }
        public async Task<PriorityDto> Handle(GetPriorityDetailRequest request, CancellationToken cancellationToken)
        {
            var Priority = await _PriorityRepository.Get(request.PriorityId);
            return _mapper.Map<PriorityDto>(Priority);
        }
    }
}

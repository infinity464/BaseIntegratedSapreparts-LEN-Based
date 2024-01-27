using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.SpareRequiredStatus;
using SchoolManagement.Application.Features.SpareRequiredStatuss.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.SpareRequiredStatuss.Handlers.Queries
{
    public class GetSpareRequiredStatusDetailRequestHandler : IRequestHandler<GetSpareRequiredStatusDetailRequest, SpareRequiredStatusDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.SpareRequiredStatus> _SpareRequiredStatusRepository;
        public GetSpareRequiredStatusDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.SpareRequiredStatus> SpareRequiredStatusRepository, IMapper mapper)
        {
            _SpareRequiredStatusRepository = SpareRequiredStatusRepository;
            _mapper = mapper;
        }
        public async Task<SpareRequiredStatusDto> Handle(GetSpareRequiredStatusDetailRequest request, CancellationToken cancellationToken)
        {
            var SpareRequiredStatus = await _SpareRequiredStatusRepository.Get(request.SpareRequiredStatusId);
            return _mapper.Map<SpareRequiredStatusDto>(SpareRequiredStatus);
        }
    }
}

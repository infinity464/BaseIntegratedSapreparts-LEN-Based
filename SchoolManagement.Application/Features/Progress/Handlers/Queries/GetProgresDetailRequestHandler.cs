using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Progres;
using SchoolManagement.Application.Features.Progress.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Progress.Handlers.Queries
{
    public class GetProgresDetailRequestHandler : IRequestHandler<GetProgresDetailRequest, ProgresDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.Progres> _ProgresRepository;
        public GetProgresDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.Progres> ProgresRepository, IMapper mapper)
        {
            _ProgresRepository = ProgresRepository;
            _mapper = mapper;
        }
        public async Task<ProgresDto> Handle(GetProgresDetailRequest request, CancellationToken cancellationToken)
        {
            var Progres = await _ProgresRepository.Get(request.ProgresId);
            return _mapper.Map<ProgresDto>(Progres);
        }
    }
}

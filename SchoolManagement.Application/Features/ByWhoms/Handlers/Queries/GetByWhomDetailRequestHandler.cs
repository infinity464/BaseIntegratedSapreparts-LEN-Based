using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ByWhom;
using SchoolManagement.Application.Features.ByWhoms.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.ByWhoms.Handlers.Queries
{
    public class GetByWhomDetailRequestHandler : IRequestHandler<GetByWhomDetailRequest, ByWhomDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.ByWhom> _ByWhomRepository;
        public GetByWhomDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.ByWhom> ByWhomRepository, IMapper mapper)
        {
            _ByWhomRepository = ByWhomRepository;
            _mapper = mapper;
        }
        public async Task<ByWhomDto> Handle(GetByWhomDetailRequest request, CancellationToken cancellationToken)
        {
            var ByWhom = await _ByWhomRepository.Get(request.ByWhomId);
            return _mapper.Map<ByWhomDto>(ByWhom);
        }
    }
}

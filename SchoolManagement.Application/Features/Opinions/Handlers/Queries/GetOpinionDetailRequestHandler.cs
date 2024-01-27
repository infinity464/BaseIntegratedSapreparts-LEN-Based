using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Opinion;
using SchoolManagement.Application.Features.Opinions.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Opinions.Handlers.Queries
{
    public class GetOpinionDetailRequestHandler : IRequestHandler<GetOpinionDetailRequest, OpinionDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.Opinion> _OpinionRepository;
        public GetOpinionDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.Opinion> OpinionRepository, IMapper mapper)
        {
            _OpinionRepository = OpinionRepository;
            _mapper = mapper;
        }
        public async Task<OpinionDto> Handle(GetOpinionDetailRequest request, CancellationToken cancellationToken)
        {
            var Opinion = await _OpinionRepository.Get(request.OpinionId);
            return _mapper.Map<OpinionDto>(Opinion);
        }
    }
}

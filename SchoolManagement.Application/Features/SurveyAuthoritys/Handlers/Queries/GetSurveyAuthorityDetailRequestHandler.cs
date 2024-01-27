using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.SurveyAuthority;
using SchoolManagement.Application.Features.SurveyAuthoritys.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.SurveyAuthoritys.Handlers.Queries
{
    public class GetSurveyAuthorityDetailRequestHandler : IRequestHandler<GetSurveyAuthorityDetailRequest, SurveyAuthorityDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.SurveyAuthority> _SurveyAuthorityRepository;
        public GetSurveyAuthorityDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.SurveyAuthority> SurveyAuthorityRepository, IMapper mapper)
        {
            _SurveyAuthorityRepository = SurveyAuthorityRepository;
            _mapper = mapper;
        }
        public async Task<SurveyAuthorityDto> Handle(GetSurveyAuthorityDetailRequest request, CancellationToken cancellationToken)
        {
            var SurveyAuthority = await _SurveyAuthorityRepository.Get(request.SurveyAuthorityId);
            return _mapper.Map<SurveyAuthorityDto>(SurveyAuthority);
        }
    }
}

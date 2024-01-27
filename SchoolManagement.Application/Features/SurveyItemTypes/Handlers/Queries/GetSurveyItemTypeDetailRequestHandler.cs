using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.SurveyItemType;
using SchoolManagement.Application.Features.SurveyItemTypes.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.SurveyItemTypes.Handlers.Queries
{
    public class GetSurveyItemTypeDetailRequestHandler : IRequestHandler<GetSurveyItemTypeDetailRequest, SurveyItemTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.SurveyItemType> _SurveyItemTypeRepository;
        public GetSurveyItemTypeDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.SurveyItemType> SurveyItemTypeRepository, IMapper mapper)
        {
            _SurveyItemTypeRepository = SurveyItemTypeRepository;
            _mapper = mapper;
        }
        public async Task<SurveyItemTypeDto> Handle(GetSurveyItemTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var SurveyItemType = await _SurveyItemTypeRepository.Get(request.SurveyItemTypeId);
            return _mapper.Map<SurveyItemTypeDto>(SurveyItemType);
        }
    }
}

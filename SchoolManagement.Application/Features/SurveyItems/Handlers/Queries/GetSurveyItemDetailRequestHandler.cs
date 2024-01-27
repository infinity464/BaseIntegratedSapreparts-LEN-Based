using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.SurveyItem;
using SchoolManagement.Application.Features.SurveyItems.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.SurveyItems.Handlers.Queries
{
    public class GetSurveyItemDetailRequestHandler : IRequestHandler<GetSurveyItemDetailRequest, SurveyItemDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.SurveyItem> _SurveyItemRepository;
        public GetSurveyItemDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.SurveyItem> SurveyItemRepository, IMapper mapper)
        {
            _SurveyItemRepository = SurveyItemRepository;
            _mapper = mapper;
        }
        public async Task<SurveyItemDto> Handle(GetSurveyItemDetailRequest request, CancellationToken cancellationToken)
        {
            var SurveyItem = await _SurveyItemRepository.Get(request.SurveyItemId);
            return _mapper.Map<SurveyItemDto>(SurveyItem);
        }
    }
}

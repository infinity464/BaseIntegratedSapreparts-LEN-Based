using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.SurveyItems.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.SurveyItems.Handlers.Queries
{
    public class GetSelectedSurveyItemRequestHandler : IRequestHandler<GetSelectedSurveyItemRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<SurveyItem> _SurveyItemRepository;


        public GetSelectedSurveyItemRequestHandler(ISchoolManagementRepository<SurveyItem> SurveyItemRepository)
        {
            _SurveyItemRepository = SurveyItemRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedSurveyItemRequest request, CancellationToken cancellationToken)
        {
            ICollection<SurveyItem> codeValues = await _SurveyItemRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Deno.Name,
                Value = x.SurveyItemId
            }).ToList();
            return selectModels;
        }
    }
}

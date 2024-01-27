using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.SurveyItemTypes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.SurveyItemTypes.Handlers.Queries
{
    public class GetSelectedSurveyItemTypeRequestHandler : IRequestHandler<GetSelectedSurveyItemTypeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<SurveyItemType> _SurveyItemTypeRepository;


        public GetSelectedSurveyItemTypeRequestHandler(ISchoolManagementRepository<SurveyItemType> SurveyItemTypeRepository)
        {
            _SurveyItemTypeRepository = SurveyItemTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedSurveyItemTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<SurveyItemType> codeValues = await _SurveyItemTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.SurveyItemTypeName,
                Value = x.SurveyItemTypeId
            }).ToList();
            return selectModels;
        }
    }
}

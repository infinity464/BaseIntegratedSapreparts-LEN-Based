using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.SurveyAuthoritys.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.SurveyAuthoritys.Handlers.Queries
{
    public class GetSelectedSurveyAuthorityRequestHandler : IRequestHandler<GetSelectedSurveyAuthorityRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<SurveyAuthority> _SurveyAuthorityRepository;


        public GetSelectedSurveyAuthorityRequestHandler(ISchoolManagementRepository<SurveyAuthority> SurveyAuthorityRepository)
        {
            _SurveyAuthorityRepository = SurveyAuthorityRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedSurveyAuthorityRequest request, CancellationToken cancellationToken)
        {
            ICollection<SurveyAuthority> codeValues = await _SurveyAuthorityRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.SurveyAuthorityName,
                Value = x.SurveyAuthorityId
            }).ToList();
            return selectModels;
        }
    }
}

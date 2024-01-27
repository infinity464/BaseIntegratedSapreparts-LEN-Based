using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.IssueTypes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.IssueTypes.Handlers.Queries
{
    public class GetSelectedIssueTypeRequestHandler : IRequestHandler<GetSelectedIssueTypeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<IssueType> _IssueTypeRepository;


        public GetSelectedIssueTypeRequestHandler(ISchoolManagementRepository<IssueType> IssueTypeRepository)
        {
            _IssueTypeRepository = IssueTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedIssueTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<IssueType> codeValues = await _IssueTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.IssueTypeId
            }).ToList();
            return selectModels;
        }
    }
}

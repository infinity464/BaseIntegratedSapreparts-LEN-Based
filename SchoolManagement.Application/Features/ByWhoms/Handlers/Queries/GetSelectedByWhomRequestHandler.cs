using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ByWhoms.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ByWhoms.Handlers.Queries
{
    public class GetSelectedByWhomRequestHandler : IRequestHandler<GetSelectedByWhomRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ByWhom> _ByWhomRepository;


        public GetSelectedByWhomRequestHandler(ISchoolManagementRepository<ByWhom> ByWhomRepository)
        {
            _ByWhomRepository = ByWhomRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedByWhomRequest request, CancellationToken cancellationToken)
        {
            ICollection<ByWhom> codeValues = await _ByWhomRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.ByWhomId
            }).ToList();
            return selectModels;
        }
    }
}

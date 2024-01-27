using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Opinions.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Opinions.Handlers.Queries
{
    public class GetSelectedOpinionRequestHandler : IRequestHandler<GetSelectedOpinionRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Opinion> _OpinionRepository;


        public GetSelectedOpinionRequestHandler(ISchoolManagementRepository<Opinion> OpinionRepository)
        {
            _OpinionRepository = OpinionRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedOpinionRequest request, CancellationToken cancellationToken)
        {
            ICollection<Opinion> codeValues = await _OpinionRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.OpinionId
            }).ToList();
            return selectModels;
        }
    }
}

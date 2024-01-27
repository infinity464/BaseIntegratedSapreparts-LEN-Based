using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Progress.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Progress.Handlers.Queries
{
    public class GetSelectedProgresRequestHandler : IRequestHandler<GetSelectedProgresRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Progres> _ProgresRepository;


        public GetSelectedProgresRequestHandler(ISchoolManagementRepository<Progres> ProgresRepository)
        {
            _ProgresRepository = ProgresRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedProgresRequest request, CancellationToken cancellationToken)
        {
            ICollection<Progres> codeValues = await _ProgresRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.ProgresId
            }).ToList();
            return selectModels;
        }
    }
}

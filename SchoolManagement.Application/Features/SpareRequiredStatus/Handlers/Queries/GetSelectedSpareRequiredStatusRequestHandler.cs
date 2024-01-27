using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.SpareRequiredStatuss.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.SpareRequiredStatuss.Handlers.Queries
{
    public class GetSelectedSpareRequiredStatusRequestHandler : IRequestHandler<GetSelectedSpareRequiredStatusRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<SpareRequiredStatus> _SpareRequiredStatusRepository;


        public GetSelectedSpareRequiredStatusRequestHandler(ISchoolManagementRepository<SpareRequiredStatus> SpareRequiredStatusRepository)
        {
            _SpareRequiredStatusRepository = SpareRequiredStatusRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedSpareRequiredStatusRequest request, CancellationToken cancellationToken)
        {
            ICollection<SpareRequiredStatus> codeValues = await _SpareRequiredStatusRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.SpareRequiredStatusId
            }).ToList();
            return selectModels;
        }
    }
}

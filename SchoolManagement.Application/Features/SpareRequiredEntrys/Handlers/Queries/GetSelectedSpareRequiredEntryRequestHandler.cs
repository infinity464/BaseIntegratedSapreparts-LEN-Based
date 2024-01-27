using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.SpareRequiredEntrys.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.SpareRequiredEntrys.Handlers.Queries
{
    public class GetSelectedSpareRequiredEntryRequestHandler : IRequestHandler<GetSelectedSpareRequiredEntryRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<SpareRequiredEntry> _SpareRequiredEntryRepository;


        public GetSelectedSpareRequiredEntryRequestHandler(ISchoolManagementRepository<SpareRequiredEntry> SpareRequiredEntryRepository)
        {
            _SpareRequiredEntryRepository = SpareRequiredEntryRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedSpareRequiredEntryRequest request, CancellationToken cancellationToken)
        {
            ICollection<SpareRequiredEntry> codeValues = await _SpareRequiredEntryRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.JobDescriptionEntryPmsId,
                Value = x.SpareRequiredEntryId
            }).ToList();
            return selectModels;
        }
    }
}

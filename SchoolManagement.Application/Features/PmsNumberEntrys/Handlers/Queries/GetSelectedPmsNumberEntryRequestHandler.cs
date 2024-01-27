using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Handlers.Queries
{
    public class GetSelectedPmsNumberEntryRequestHandler : IRequestHandler<GetSelectedPmsNumberEntryRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<PmsNumberEntry> _PmsNumberEntryRepository;


        public GetSelectedPmsNumberEntryRequestHandler(ISchoolManagementRepository<PmsNumberEntry> PmsNumberEntryRepository)
        {
            _PmsNumberEntryRepository = PmsNumberEntryRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedPmsNumberEntryRequest request, CancellationToken cancellationToken)
        {
            ICollection<PmsNumberEntry> codeValues = await _PmsNumberEntryRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.PmsNo,
                Value = x.PmsNumberEntryId
            }).ToList();
            return selectModels;
        }
    }
}

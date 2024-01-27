using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Handlers.Queries
{
    public class GetSelectedPmsNoByItemDetailIdRequestHandler : IRequestHandler<GetSelectedPmsNoByItemDetailIdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<PmsNumberEntry> _PmsNumberEntryRepository;

        public GetSelectedPmsNoByItemDetailIdRequestHandler(ISchoolManagementRepository<PmsNumberEntry> PmsNumberEntryRepository)
        {
            _PmsNumberEntryRepository = PmsNumberEntryRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedPmsNoByItemDetailIdRequest request, CancellationToken cancellationToken)
        {
            ICollection<PmsNumberEntry> codeValues = await _PmsNumberEntryRepository.FilterAsync(x => x.IsActive && x.ItemDetailId==request.ItemDetailId);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.PmsNo,
                Value = x.PmsNumberEntryId
            }).ToList();
            return selectModels;
        }
    }
}

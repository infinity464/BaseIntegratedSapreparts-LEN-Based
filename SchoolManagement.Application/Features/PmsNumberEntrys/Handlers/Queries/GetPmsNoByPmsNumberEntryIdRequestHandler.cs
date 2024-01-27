using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Handlers.Queries
{
    public class GetPmsNoByPmsNumberEntryIdRequestHandler : IRequestHandler<GetPmsNoByPmsNumberEntryIdRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<PmsNumberEntry> _PmsNumberEntryRepository;

          
        public GetPmsNoByPmsNumberEntryIdRequestHandler(ISchoolManagementRepository<PmsNumberEntry> PmsNumberEntryRepository)
        {
            _PmsNumberEntryRepository = PmsNumberEntryRepository;           
        }

        public async Task<List<SelectedModel>> Handle(GetPmsNoByPmsNumberEntryIdRequest request, CancellationToken cancellationToken)
        {
            ICollection<PmsNumberEntry> PmsNumberEntrys = await _PmsNumberEntryRepository.FilterAsync(x =>x.PmsNumberEntryId == request.PmsNumberEntryId);
            List<SelectedModel> selectModels = PmsNumberEntrys.Select(x => new SelectedModel
            {
                Text = x.PmsNo, 
                Value = x.PmsNumberEntryId 
            }).ToList();
            return selectModels;
        }
    }
}

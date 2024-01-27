using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Handlers.Queries
{
    public class GetSelectedItemNameFromPmsNumberEntryRequestHandler : IRequestHandler<GetSelectedItemNameFromPmsNumberEntryRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<PmsNumberEntry> _PmsNumberEntryRepository;


        public GetSelectedItemNameFromPmsNumberEntryRequestHandler(ISchoolManagementRepository<PmsNumberEntry> PmsNumberEntryRepository)
        {
            _PmsNumberEntryRepository = PmsNumberEntryRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedItemNameFromPmsNumberEntryRequest request, CancellationToken cancellationToken)
        {
            
            IQueryable<PmsNumberEntry> PmsNumberEntrys = _PmsNumberEntryRepository.FilterWithInclude(x => x.UnitId == request.UnitId && x.DepartmentNameId == request.DepartmentNameId && x.IsActive, "ItemDetail");
            List<SelectedModel> selectModels = PmsNumberEntrys.Select(x => new SelectedModel 
            {
                Text = x.ItemDetail.NameOfItem,
                Value = x.PmsNumberEntryId
            }).ToList();
            return selectModels;
        }
    }
}

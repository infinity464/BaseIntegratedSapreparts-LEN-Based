using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.Demands.Requests.Queries;
using SchoolManagement.Application.Features.Procurements.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.Procurements.Handlers.Queries
{
    public class GetSelectedProcurementQtyFromProcurementByParameterRequestHandler : IRequestHandler<GetSelectedProcurementQtyFromProcurementByParameterRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<Procurement> _ProcurementRepository;


        public GetSelectedProcurementQtyFromProcurementByParameterRequestHandler(ISchoolManagementRepository<Procurement> ProcurementRepository)
        {
            _ProcurementRepository = ProcurementRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedProcurementQtyFromProcurementByParameterRequest request, CancellationToken cancellationToken)
        {
            IQueryable<Procurement> Procurements = _ProcurementRepository.FilterWithInclude((x => x.IsActive && x.UnitId == request.UnitId && x.DepartmentNameId==request.DepartmentNameId && x.ItemCategoryId==request.ItemCategoryId && x.DemandId == request.DemandId), "ItemDetail");
            List<SelectedModel> selectModels = Procurements.Select(x => new SelectedModel 
            {
                Text = x.Qty,
                Value = x.ProcurementId
            }).ToList();
            return selectModels;
        }
    }
}

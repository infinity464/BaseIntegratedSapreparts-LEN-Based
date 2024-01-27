using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.SpareRequisitions.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.SpareRequisitions.Handlers.Queries
{
    public class GetSelectedSpareRequisitionRequestHandler : IRequestHandler<GetSelectedSpareRequisitionRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<SpareRequisition> _SpareRequisitionRepository;


        public GetSelectedSpareRequisitionRequestHandler(ISchoolManagementRepository<SpareRequisition> SpareRequisitionRepository)
        {
            _SpareRequisitionRepository = SpareRequisitionRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedSpareRequisitionRequest request, CancellationToken cancellationToken)
        {
            ICollection<SpareRequisition> codeValues = await _SpareRequisitionRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.SpareRequisitionId
            }).ToList();
            return selectModels;
        }
    }
}

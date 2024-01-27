using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.CorrectiveMaintenences.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.CorrectiveMaintenences.Handlers.Queries
{
    public class GetSelectedCorrectiveMaintenenceRequestHandler : IRequestHandler<GetSelectedCorrectiveMaintenenceRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<CorrectiveMaintenence> _CorrectiveMaintenenceRepository;


        public GetSelectedCorrectiveMaintenenceRequestHandler(ISchoolManagementRepository<CorrectiveMaintenence> CorrectiveMaintenenceRepository)
        {
            _CorrectiveMaintenenceRepository = CorrectiveMaintenenceRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedCorrectiveMaintenenceRequest request, CancellationToken cancellationToken)
        {
            ICollection<CorrectiveMaintenence> codeValues = await _CorrectiveMaintenenceRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.ByWhomId,
                Value = x.CorrectiveMaintenenceId
            }).ToList();
            return selectModels;
        }
    }
}

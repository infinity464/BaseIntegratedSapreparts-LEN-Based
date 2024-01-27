using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Handlers.Queries
{
    public class GetSelectedScheduleMaintenenceTypeRequestHandler : IRequestHandler<GetSelectedScheduleMaintenenceTypeRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ScheduleMaintenenceType> _ScheduleMaintenenceTypeRepository;


        public GetSelectedScheduleMaintenenceTypeRequestHandler(ISchoolManagementRepository<ScheduleMaintenenceType> ScheduleMaintenenceTypeRepository)
        {
            _ScheduleMaintenenceTypeRepository = ScheduleMaintenenceTypeRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedScheduleMaintenenceTypeRequest request, CancellationToken cancellationToken)
        {
            ICollection<ScheduleMaintenenceType> codeValues = await _ScheduleMaintenenceTypeRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.ScheduleMaintenenceTypeId
            }).ToList();
            return selectModels;
        }
    }
}

using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ReturnStatuss.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ReturnStatuss.Handlers.Queries
{
    public class GetSelectedReturnStatusRequestHandler : IRequestHandler<GetSelectedReturnStatusRequest, List<SelectedModel>>
    {
        private readonly ISchoolManagementRepository<ReturnStatus> _ReturnStatusRepository;


        public GetSelectedReturnStatusRequestHandler(ISchoolManagementRepository<ReturnStatus> ReturnStatusRepository)
        {
            _ReturnStatusRepository = ReturnStatusRepository;
        }

        public async Task<List<SelectedModel>> Handle(GetSelectedReturnStatusRequest request, CancellationToken cancellationToken)
        {
            ICollection<ReturnStatus> codeValues = await _ReturnStatusRepository.FilterAsync(x => x.IsActive);
            List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
            {
                Text = x.Name,
                Value = x.ReturnStatusId
            }).ToList();
            return selectModels;
        }
    }
}

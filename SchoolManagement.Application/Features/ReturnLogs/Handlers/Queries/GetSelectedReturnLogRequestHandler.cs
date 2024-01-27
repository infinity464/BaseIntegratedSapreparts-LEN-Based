using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Features.ReturnLogs.Requests.Queries;
using SchoolManagement.Domain;
using SchoolManagement.Shared.Models;

namespace SchoolManagement.Application.Features.ReturnLogs.Handlers.Queries
{
    //public class GetSelectedReturnLogRequestHandler : IRequestHandler<GetSelectedReturnLogRequest, List<SelectedModel>>
    //{
        //private readonly ISchoolManagementRepository<ReturnLog> _ReturnLogRepository;


        //public GetSelectedReturnLogRequestHandler(ISchoolManagementRepository<ReturnLog> ReturnLogRepository)
        //{
        //    _ReturnLogRepository = ReturnLogRepository;
        //}

        //public async Task<List<SelectedModel>> Handle(GetSelectedReturnLogRequest request, CancellationToken cancellationToken)
        //{
        //    ICollection<ReturnLog> codeValues = await _ReturnLogRepository.FilterAsync(x => x.IsActive);
        //    List<SelectedModel> selectModels = codeValues.Select(x => new SelectedModel
        //    {
        //        Text = x.Name,
        //        Value = x.ReturnLogId
        //    }).ToList();
        //    return selectModels;
        //}
    //}
}

using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.ReturnLogs;
using SchoolManagement.Application.Features.ReturnLogs.Requests.Queries;

namespace SchoolManagement.Application.Features.ReturnLogs.Handlers.Queries
{
    public class GetReturnLogListRequestHandler : IRequestHandler<GetReturnLogListRequest, PagedResult<ReturnLogDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.ReturnLog> _ReturnLogRepository;

        private readonly IMapper _mapper;

        public GetReturnLogListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.ReturnLog> ReturnLogRepository, IMapper mapper)
        {
            _ReturnLogRepository = ReturnLogRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ReturnLogDto>> Handle(GetReturnLogListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.ReturnLog> ReturnLogs = _ReturnLogRepository.FilterWithInclude(x => String.IsNullOrEmpty(request.QueryParams.SearchText));
            var totalCount = ReturnLogs.Count();
            ReturnLogs = ReturnLogs.OrderByDescending(x => x.ReturnLogId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ReturnLogDtos = _mapper.Map<List<ReturnLogDto>>(ReturnLogs);
            var result = new PagedResult<ReturnLogDto>(ReturnLogDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

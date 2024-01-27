using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ReturnStatuss.Requests.Queries;
using SchoolManagement.Application.DTOs.ReturnStatus;

namespace SchoolManagement.Application.Features.ReturnStatuss.Handlers.Queries
{
    public class GetReturnStatusListRequestHandler : IRequestHandler<GetReturnStatusListRequest, PagedResult<ReturnStatusDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.ReturnStatus> _ReturnStatusRepository;

        private readonly IMapper _mapper;

        public GetReturnStatusListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.ReturnStatus> ReturnStatusRepository, IMapper mapper)
        {
            _ReturnStatusRepository = ReturnStatusRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ReturnStatusDto>> Handle(GetReturnStatusListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.ReturnStatus> ReturnStatuss = _ReturnStatusRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = ReturnStatuss.Count();
            ReturnStatuss = ReturnStatuss.OrderByDescending(x => x.ReturnStatusId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ReturnStatusDtos = _mapper.Map<List<ReturnStatusDto>>(ReturnStatuss);
            var result = new PagedResult<ReturnStatusDto>(ReturnStatusDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

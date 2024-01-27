using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.SpareRequiredStatuss.Requests.Queries;
using SchoolManagement.Application.DTOs.SpareRequiredStatus;

namespace SchoolManagement.Application.Features.SpareRequiredStatuss.Handlers.Queries
{
    public class GetSpareRequiredStatusListRequestHandler : IRequestHandler<GetSpareRequiredStatusListRequest, PagedResult<SpareRequiredStatusDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.SpareRequiredStatus> _SpareRequiredStatusRepository;

        private readonly IMapper _mapper;

        public GetSpareRequiredStatusListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.SpareRequiredStatus> SpareRequiredStatusRepository, IMapper mapper)
        {
            _SpareRequiredStatusRepository = SpareRequiredStatusRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<SpareRequiredStatusDto>> Handle(GetSpareRequiredStatusListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.SpareRequiredStatus> SpareRequiredStatuss = _SpareRequiredStatusRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = SpareRequiredStatuss.Count();
            SpareRequiredStatuss = SpareRequiredStatuss.OrderByDescending(x => x.SpareRequiredStatusId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var SpareRequiredStatusDtos = _mapper.Map<List<SpareRequiredStatusDto>>(SpareRequiredStatuss);
            var result = new PagedResult<SpareRequiredStatusDto>(SpareRequiredStatusDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.ByWhom;
using SchoolManagement.Application.Features.ByWhoms.Requests.Queries;

namespace SchoolManagement.Application.Features.ByWhoms.Handlers.Queries
{
    public class GetByWhomListRequestHandler : IRequestHandler<GetByWhomListRequest, PagedResult<ByWhomDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.ByWhom> _ByWhomRepository;

        private readonly IMapper _mapper;

        public GetByWhomListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.ByWhom> ByWhomRepository, IMapper mapper)
        {
            _ByWhomRepository = ByWhomRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<ByWhomDto>> Handle(GetByWhomListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.ByWhom> ByWhoms = _ByWhomRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = ByWhoms.Count();
            ByWhoms = ByWhoms.OrderByDescending(x => x.ByWhomId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var ByWhomDtos = _mapper.Map<List<ByWhomDto>>(ByWhoms);
            var result = new PagedResult<ByWhomDto>(ByWhomDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

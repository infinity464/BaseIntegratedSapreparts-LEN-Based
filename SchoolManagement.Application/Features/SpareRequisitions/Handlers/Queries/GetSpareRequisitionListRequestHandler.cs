using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.SpareRequisitions.Requests.Queries;
using SchoolManagement.Application.DTOs.SpareRequisition;

namespace SchoolManagement.Application.Features.SpareRequisitions.Handlers.Queries
{
    public class GetSpareRequisitionListRequestHandler : IRequestHandler<GetSpareRequisitionListRequest, PagedResult<SpareRequisitionDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.SpareRequisition> _SpareRequisitionRepository;

        private readonly IMapper _mapper;

        public GetSpareRequisitionListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.SpareRequisition> SpareRequisitionRepository, IMapper mapper)
        {
            _SpareRequisitionRepository = SpareRequisitionRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<SpareRequisitionDto>> Handle(GetSpareRequisitionListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.SpareRequisition> SpareRequisitions = _SpareRequisitionRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = SpareRequisitions.Count();
            SpareRequisitions = SpareRequisitions.OrderByDescending(x => x.SpareRequisitionId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var SpareRequisitionDtos = _mapper.Map<List<SpareRequisitionDto>>(SpareRequisitions);
            var result = new PagedResult<SpareRequisitionDto>(SpareRequisitionDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

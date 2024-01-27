using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Models;
using MediatR;
using AutoMapper;
using SchoolManagement.Application.DTOs.Common.Validators;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.DTOs.Opinion;
using SchoolManagement.Application.Features.Opinions.Requests.Queries;

namespace SchoolManagement.Application.Features.Opinions.Handlers.Queries
{
    public class GetOpinionListRequestHandler : IRequestHandler<GetOpinionListRequest, PagedResult<OpinionDto>>
    {

        private readonly ISchoolManagementRepository<SchoolManagement.Domain.Opinion> _OpinionRepository;

        private readonly IMapper _mapper;

        public GetOpinionListRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.Opinion> OpinionRepository, IMapper mapper)
        {
            _OpinionRepository = OpinionRepository;
            _mapper = mapper;
        }

        public async Task<PagedResult<OpinionDto>> Handle(GetOpinionListRequest request, CancellationToken cancellationToken)
        {
            var validator = new QueryParamsValidator();
            var validationResult = await validator.ValidateAsync(request.QueryParams);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            IQueryable<SchoolManagement.Domain.Opinion> Opinions = _OpinionRepository.FilterWithInclude(x => (x.Name.Contains(request.QueryParams.SearchText) || String.IsNullOrEmpty(request.QueryParams.SearchText)));
            var totalCount = Opinions.Count();
            Opinions = Opinions.OrderByDescending(x => x.OpinionId).Skip((request.QueryParams.PageNumber - 1) * request.QueryParams.PageSize).Take(request.QueryParams.PageSize);

            var OpinionDtos = _mapper.Map<List<OpinionDto>>(Opinions);
            var result = new PagedResult<OpinionDto>(OpinionDtos, totalCount, request.QueryParams.PageNumber, request.QueryParams.PageSize);

            return result;


        }
    }
}

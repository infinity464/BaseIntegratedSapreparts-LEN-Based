using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.PmsNumberEntry;
using SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Queries;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Handlers.Queries
{
    public class GetPmsNumberEntryByDepartmentRequestHandler : IRequestHandler<GetPmsNumberEntryByDepartmentRequest, List<PmsNumberEntryDto>>
    {
        private readonly ISchoolManagementRepository<PmsNumberEntry> _PmsNumberEntryRepository;

        private readonly IMapper _mapper;
        public GetPmsNumberEntryByDepartmentRequestHandler(ISchoolManagementRepository<PmsNumberEntry> PmsNumberEntryRepository, IMapper mapper)
        {
            _PmsNumberEntryRepository = PmsNumberEntryRepository;
            _mapper = mapper;
        }

        public async Task<List<PmsNumberEntryDto>> Handle(GetPmsNumberEntryByDepartmentRequest request, CancellationToken cancellationToken)
        {
            IQueryable<PmsNumberEntry> PmsNumberEntrys = _PmsNumberEntryRepository.FilterWithInclude(x => x.DepartmentNameId == request.DepartmentNameId, "DepartmentName", "ItemDetail");

            var PmsNumberEntryDtos = _mapper.Map<List<PmsNumberEntryDto>>(PmsNumberEntrys);

            return PmsNumberEntryDtos;
        }

    }
}

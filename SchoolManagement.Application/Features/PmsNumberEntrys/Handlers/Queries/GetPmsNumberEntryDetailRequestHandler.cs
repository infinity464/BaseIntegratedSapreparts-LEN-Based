using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.PmsNumberEntry;
using SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Handlers.Queries
{
    public class GetPmsNumberEntryDetailRequestHandler : IRequestHandler<GetPmsNumberEntryDetailRequest, PmsNumberEntryDto>
    {
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<SchoolManagement.Domain.PmsNumberEntry> _PmsNumberEntryRepository;
        public GetPmsNumberEntryDetailRequestHandler(ISchoolManagementRepository<SchoolManagement.Domain.PmsNumberEntry> PmsNumberEntryRepository, IMapper mapper)
        {
            _PmsNumberEntryRepository = PmsNumberEntryRepository;
            _mapper = mapper;
        }
        public async Task<PmsNumberEntryDto> Handle(GetPmsNumberEntryDetailRequest request, CancellationToken cancellationToken)
        {
            var PmsNumberEntry = await _PmsNumberEntryRepository.Get(request.PmsNumberEntryId);
            return _mapper.Map<PmsNumberEntryDto>(PmsNumberEntry);
        }
    }
}

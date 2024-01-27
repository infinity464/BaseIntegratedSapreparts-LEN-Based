using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.SpareRequiredEntrys.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.SpareRequiredEntrys.Handlers.Commands
{
    public class DeleteSpareRequiredEntryCommandHandler : IRequestHandler<DeleteSpareRequiredEntryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteSpareRequiredEntryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteSpareRequiredEntryCommand request, CancellationToken cancellationToken)
        {
            var SpareRequiredEntry = await _unitOfWork.Repository<SpareRequiredEntry>().Get(request.SpareRequiredEntryId);

            if (SpareRequiredEntry == null)
                throw new NotFoundException(nameof(SpareRequiredEntry), request.SpareRequiredEntryId);

            await _unitOfWork.Repository<SpareRequiredEntry>().Delete(SpareRequiredEntry);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

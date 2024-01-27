using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.PmsNumberEntrys.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.PmsNumberEntrys.Handlers.Commands
{
    public class DeletePmsNumberEntryCommandHandler : IRequestHandler<DeletePmsNumberEntryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeletePmsNumberEntryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeletePmsNumberEntryCommand request, CancellationToken cancellationToken)
        {
            var PmsNumberEntry = await _unitOfWork.Repository<PmsNumberEntry>().Get(request.PmsNumberEntryId);

            if (PmsNumberEntry == null)
                throw new NotFoundException(nameof(PmsNumberEntry), request.PmsNumberEntryId);

            await _unitOfWork.Repository<PmsNumberEntry>().Delete(PmsNumberEntry);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ReturnStatuss.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ReturnStatuss.Handlers.Commands
{
    public class DeleteReturnStatusCommandHandler : IRequestHandler<DeleteReturnStatusCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteReturnStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteReturnStatusCommand request, CancellationToken cancellationToken)
        {
            var ReturnStatus = await _unitOfWork.Repository<ReturnStatus>().Get(request.ReturnStatusId);

            if (ReturnStatus == null)
                throw new NotFoundException(nameof(ReturnStatus), request.ReturnStatusId);

            await _unitOfWork.Repository<ReturnStatus>().Delete(ReturnStatus);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

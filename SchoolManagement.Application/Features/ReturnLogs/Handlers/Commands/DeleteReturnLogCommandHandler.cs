using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ReturnLogs.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ReturnLogs.Handlers.Commands
{
    public class DeleteReturnLogCommandHandler : IRequestHandler<DeleteReturnLogCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteReturnLogCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteReturnLogCommand request, CancellationToken cancellationToken)
        {
            var ReturnLog = await _unitOfWork.Repository<ReturnLog>().Get(request.ReturnLogId);

            if (ReturnLog == null)
                throw new NotFoundException(nameof(ReturnLog), request.ReturnLogId);

            await _unitOfWork.Repository<ReturnLog>().Delete(ReturnLog);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

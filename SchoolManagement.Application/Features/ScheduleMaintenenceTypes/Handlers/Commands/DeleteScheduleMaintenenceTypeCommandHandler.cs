using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Handlers.Commands
{
    public class DeleteScheduleMaintenenceTypeCommandHandler : IRequestHandler<DeleteScheduleMaintenenceTypeCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteScheduleMaintenenceTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteScheduleMaintenenceTypeCommand request, CancellationToken cancellationToken)
        {
            var ScheduleMaintenenceType = await _unitOfWork.Repository<ScheduleMaintenenceType>().Get(request.ScheduleMaintenenceTypeId);

            if (ScheduleMaintenenceType == null)
                throw new NotFoundException(nameof(ScheduleMaintenenceType), request.ScheduleMaintenenceTypeId);

            await _unitOfWork.Repository<ScheduleMaintenenceType>().Delete(ScheduleMaintenenceType);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Commands;
using SchoolManagement.Application.Features.ItemStors.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ItemStors.Handlers.Commands
{
    public class ScheduleMaintenenceCompletedCommandHandler : IRequestHandler<ScheduleMaintenenceCompletedCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 

        public ScheduleMaintenenceCompletedCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
         
        public async Task<Unit> Handle(ScheduleMaintenenceCompletedCommand request, CancellationToken cancellationToken)
        {
            var ScheduleMaintenence = await _unitOfWork.Repository<ScheduleMaintenance>().Get(request.ScheduleMaintenenceCompletedDto.ScheduleMaintenanceId);

            ScheduleMaintenence.DrNo = request.ScheduleMaintenenceCompletedDto.DrNo;
            ScheduleMaintenence.ByWhomId = request.ScheduleMaintenenceCompletedDto.ByWhomId;
            ScheduleMaintenence.StartDate = request.ScheduleMaintenenceCompletedDto.StartDate;
            ScheduleMaintenence.ProgresId = request.ScheduleMaintenenceCompletedDto.ProgresId;
            ScheduleMaintenence.CompletedDate = DateTime.Now;

            ScheduleMaintenence.CompletedStatus = 1;

            await _unitOfWork.Repository<ScheduleMaintenance>().Update(ScheduleMaintenence);
            await _unitOfWork.Save();

            //Get issue register and update CorrectiveCompleteStatus
            var issueRegister = await _unitOfWork.Repository<IssueRegister>().Get((int)request.ScheduleMaintenenceCompletedDto.IssueRegisterId);
            issueRegister.ScheduleCompleteStatus = 0;

            await _unitOfWork.Repository<IssueRegister>().Update(issueRegister);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

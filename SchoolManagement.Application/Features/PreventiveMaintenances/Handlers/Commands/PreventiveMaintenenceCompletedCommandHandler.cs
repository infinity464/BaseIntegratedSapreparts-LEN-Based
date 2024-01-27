using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Commands;
using SchoolManagement.Application.Features.ItemStors.Requests.Commands;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ItemStors.Handlers.Commands
{
    public class PreventiveMaintenenceCompletedCommandHandler : IRequestHandler<PreventiveMaintenenceCompletedCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 

        public PreventiveMaintenenceCompletedCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(PreventiveMaintenenceCompletedCommand request, CancellationToken cancellationToken)
        {
            var PreventiveMaintenence = await _unitOfWork.Repository<PreventiveMaintenance>().Get(request.PreventiveMaintenenceCompletedDto.PreventiveMaintenanceId);
             
            PreventiveMaintenence.CompletedStatus = 1;
            PreventiveMaintenence.CompletedDate = request.PreventiveMaintenenceCompletedDto.CompletedDate;

            await _unitOfWork.Repository<PreventiveMaintenance>().Update(PreventiveMaintenence);
            await _unitOfWork.Save();

            //Get issue register and update CorrectiveCompleteStatus
            var issueRegister = await _unitOfWork.Repository<IssueRegister>().Get((int)request.PreventiveMaintenenceCompletedDto.IssueRegisterId);
            issueRegister.PreventiveCompleteStatus = 0;

            await _unitOfWork.Repository<IssueRegister>().Update(issueRegister);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

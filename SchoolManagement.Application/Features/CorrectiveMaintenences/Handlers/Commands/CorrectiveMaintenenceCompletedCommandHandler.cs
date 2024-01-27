using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Commands;
using SchoolManagement.Application.Features.ItemStors.Requests.Commands;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ItemStors.Handlers.Commands
{
    public class CorrectiveMaintenenceCompletedCommandHandler : IRequestHandler<CorrectiveMaintenenceCompletedCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; 

        public CorrectiveMaintenenceCompletedCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CorrectiveMaintenenceCompletedCommand request, CancellationToken cancellationToken)
        {
            var correctiveMaintenence = await _unitOfWork.Repository<CorrectiveMaintenence>().Get(request.CorrectiveMaintenenceCompletedDto.CorrectiveMaintenenceId);

            correctiveMaintenence.CompletedStatus = 1;
            correctiveMaintenence.WorkCompletedDate = request.CorrectiveMaintenenceCompletedDto.WorkCompletedDate;
            correctiveMaintenence.StatusOfJobId = request.CorrectiveMaintenenceCompletedDto.StatusOfJobId;
            correctiveMaintenence.WorkCompletedDate = correctiveMaintenence.WorkCompletedDate.Value.AddDays(1.0);

            await _unitOfWork.Repository<CorrectiveMaintenence>().Update(correctiveMaintenence);
            await _unitOfWork.Save();

            //Get issue register and update CorrectiveCompleteStatus
            var issueRegister = await _unitOfWork.Repository<IssueRegister>().Get((int)request.CorrectiveMaintenenceCompletedDto.IssueRegisterId);
            issueRegister.CorrectiveCompleteStatus = 0;

            await _unitOfWork.Repository<IssueRegister>().Update(issueRegister);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ScheduleMaintenances.Validators;
using SchoolManagement.Application.Features.ScheduleMaintenances.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ScheduleMaintenances.Handlers.Commands
{
    public class CreateScheduleMaintenanceCommandHandler : IRequestHandler<CreateScheduleMaintenanceCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateScheduleMaintenanceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateScheduleMaintenanceCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateScheduleMaintenanceDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ScheduleMaintenanceDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var ScheduleMaintenance = _mapper.Map<ScheduleMaintenance>(request.ScheduleMaintenanceDto);

                ScheduleMaintenance = await _unitOfWork.Repository<ScheduleMaintenance>().Add(ScheduleMaintenance);
                ScheduleMaintenance.CompletedStatus = 0;
                //ScheduleMaintenance.CompletedDate = ScheduleMaintenance.CompletedDate.Value.AddDays(1.0);
                ScheduleMaintenance.StartDate = ScheduleMaintenance.StartDate.Value.AddDays(1.0);

                try
                {
                    await _unitOfWork.Save();

                    //Get issue register and update CorrectiveCompleteStatus
                    var issueRegister = await _unitOfWork.Repository<IssueRegister>().Get((int)request.ScheduleMaintenanceDto.IssueRegisterId);
                    issueRegister.ScheduleCompleteStatus = 1;
                  

                    await _unitOfWork.Repository<IssueRegister>().Update(issueRegister);
                    await _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = ScheduleMaintenance.ScheduleMaintenanceId;
            }

            return response;
        }
    }
}

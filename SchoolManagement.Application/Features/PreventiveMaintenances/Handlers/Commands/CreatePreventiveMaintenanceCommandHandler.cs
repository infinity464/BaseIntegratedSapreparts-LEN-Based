using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.PreventiveMaintenance.Validators;
using SchoolManagement.Application.Features.PreventiveMaintenances.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.PreventiveMaintenances.Handlers.Commands
{
    public class CreatePreventiveMaintenanceCommandHandler : IRequestHandler<CreatePreventiveMaintenanceCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreatePreventiveMaintenanceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreatePreventiveMaintenanceCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreatePreventiveMaintenanceDtoValidator();
            var validationResult = await validator.ValidateAsync(request.PreventiveMaintenanceDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var PreventiveMaintenance = _mapper.Map<PreventiveMaintenance>(request.PreventiveMaintenanceDto);

                PreventiveMaintenance = await _unitOfWork.Repository<PreventiveMaintenance>().Add(PreventiveMaintenance);
                PreventiveMaintenance.CompletedStatus = 1;
                PreventiveMaintenance.CompletedDate = PreventiveMaintenance.CompletedDate.Value.AddDays(1.0);

                try
                {
                    await _unitOfWork.Save();

                    if(request.PreventiveMaintenanceDto.IssueRegisterId != null)
                    {
                        //Get issue register and update CorrectiveCompleteStatus
                        var issueRegister = await _unitOfWork.Repository<IssueRegister>().Get((int)request.PreventiveMaintenanceDto.IssueRegisterId);
                        issueRegister.PreventiveCompleteStatus = 1;

                        await _unitOfWork.Repository<IssueRegister>().Update(issueRegister);
                        await _unitOfWork.Save();
                    }

                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = PreventiveMaintenance.PreventiveMaintenanceId;
            }

            return response;
        }
    }
}

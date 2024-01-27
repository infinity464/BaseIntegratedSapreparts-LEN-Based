using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.JobDescriptionScheduleMaintenences.Validators;
using SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.JobDescriptionScheduleMaintenences.Handlers.Commands
{
    public class CreateJobDescriptionScheduleMaintenenceCommandHandler : IRequestHandler<CreateJobDescriptionScheduleMaintenenceCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateJobDescriptionScheduleMaintenenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateJobDescriptionScheduleMaintenenceCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateJobDescriptionScheduleMaintenenceDtoValidator();
            var validationResult = await validator.ValidateAsync(request.JobDescriptionScheduleMaintenenceDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var JobDescriptionScheduleMaintenence = _mapper.Map<JobDescriptionScheduleMaintenence>(request.JobDescriptionScheduleMaintenenceDto);

                JobDescriptionScheduleMaintenence = await _unitOfWork.Repository<JobDescriptionScheduleMaintenence>().Add(JobDescriptionScheduleMaintenence);

                try
                {
                    await _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine(ex);
                }


                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = JobDescriptionScheduleMaintenence.JobDescriptionScheduleMaintenenceId;
            }

            return response;
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.JobCategoryMaintenences.Validators;
using SchoolManagement.Application.Features.JobCategoryMaintenences.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.JobCategoryMaintenences.Handlers.Commands
{
    public class CreateJobCategoryMaintenenceCommandHandler : IRequestHandler<CreateJobCategoryMaintenenceCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateJobCategoryMaintenenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateJobCategoryMaintenenceCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateJobCategoryMaintenenceDtoValidator();
            var validationResult = await validator.ValidateAsync(request.JobCategoryMaintenenceDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var JobCategoryMaintenence = _mapper.Map<JobCategoryMaintenence>(request.JobCategoryMaintenenceDto);

                JobCategoryMaintenence = await _unitOfWork.Repository<JobCategoryMaintenence>().Add(JobCategoryMaintenence);

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
                response.Id = JobCategoryMaintenence.JobCategoryMaintenenceId;
            }

            return response;
        }
    }
}

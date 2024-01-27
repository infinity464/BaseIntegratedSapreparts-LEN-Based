using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.JobDescriptionEntryPms.Validators;
using SchoolManagement.Application.Features.JobDescriptionEntryPmses.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.JobDescriptionEntryPmses.Handlers.Commands
{
    public class CreateJobDescriptionEntryPmsCommandHandler : IRequestHandler<CreateJobDescriptionEntryPmsCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateJobDescriptionEntryPmsCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateJobDescriptionEntryPmsCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateJobDescriptionEntryPmsDtoValidator();
            var validationResult = await validator.ValidateAsync(request.JobDescriptionEntryPmsDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var JobDescriptionEntryPms = _mapper.Map<JobDescriptionEntryPms>(request.JobDescriptionEntryPmsDto);

                JobDescriptionEntryPms = await _unitOfWork.Repository<JobDescriptionEntryPms>().Add(JobDescriptionEntryPms);

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
                response.Id = JobDescriptionEntryPms.JobDescriptionEntryPmsId;
            }

            return response;
        }
    }
}

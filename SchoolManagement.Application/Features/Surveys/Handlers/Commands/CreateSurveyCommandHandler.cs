using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Survey.Validators;
using SchoolManagement.Application.Features.Surveys.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Surveys.Handlers.Commands
{
    public class CreateSurveyCommandHandler : IRequestHandler<CreateSurveyCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateSurveyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateSurveyCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateSurveyDtoValidator();
            var validationResult = await validator.ValidateAsync(request.SurveyDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var Survey = _mapper.Map<Survey>(request.SurveyDto);

                Survey = await _unitOfWork.Repository<Survey>().Add(Survey);
                Survey.DemandCompleteStatus = 0;

                 
                var issueRegister = await _unitOfWork.Repository<IssueRegister>().Get(Survey.IssueRegisterId.Value);
                issueRegister.SurveyStatus = 0;

                Survey.ItemCategoryId = issueRegister.ItemCategoryId;
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
                response.Id = Survey.SurveyId;
            }

            return response;
        }
    }
}

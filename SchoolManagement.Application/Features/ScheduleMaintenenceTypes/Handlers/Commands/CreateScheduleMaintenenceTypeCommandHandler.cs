using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.ScheduleMaintenenceTypes.Validators;
using SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.ScheduleMaintenenceTypes.Handlers.Commands
{
    public class CreateScheduleMaintenenceTypeCommandHandler : IRequestHandler<CreateScheduleMaintenenceTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateScheduleMaintenenceTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateScheduleMaintenenceTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateScheduleMaintenenceTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.ScheduleMaintenenceTypeDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var ScheduleMaintenenceType = _mapper.Map<ScheduleMaintenenceType>(request.ScheduleMaintenenceTypeDto);

                ScheduleMaintenenceType = await _unitOfWork.Repository<ScheduleMaintenenceType>().Add(ScheduleMaintenenceType);

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
                response.Id = ScheduleMaintenenceType.ScheduleMaintenenceTypeId;
            }

            return response;
        }
    }
}

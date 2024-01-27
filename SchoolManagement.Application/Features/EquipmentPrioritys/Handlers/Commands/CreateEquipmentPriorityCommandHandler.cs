using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.EquipmentPrioritys.Validators;
using SchoolManagement.Application.Features.EquipmentPrioritys.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.EquipmentPrioritys.Handlers.Commands
{
    public class CreateEquipmentPriorityCommandHandler : IRequestHandler<CreateEquipmentPriorityCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateEquipmentPriorityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateEquipmentPriorityCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateEquipmentPriorityDtoValidator();
            var validationResult = await validator.ValidateAsync(request.EquipmentPriorityDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var EquipmentPriority = _mapper.Map<EquipmentPriority>(request.EquipmentPriorityDto);

                EquipmentPriority = await _unitOfWork.Repository<EquipmentPriority>().Add(EquipmentPriority);

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
                response.Id = EquipmentPriority.EquipmentPriorityId;
            }

            return response;
        }
    }
}

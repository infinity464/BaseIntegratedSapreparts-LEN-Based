using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.MaintenanceAuthority.Validators;
using SchoolManagement.Application.Features.MaintenanceAuthoritys.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.MaintenanceAuthoritys.Handlers.Commands
{
    public class CreateMaintenanceAuthorityCommandHandler : IRequestHandler<CreateMaintenanceAuthorityCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMaintenanceAuthorityCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateMaintenanceAuthorityCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateMaintenanceAuthorityDtoValidator();
            var validationResult = await validator.ValidateAsync(request.MaintenanceAuthorityDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var MaintenanceAuthority = _mapper.Map<MaintenanceAuthority>(request.MaintenanceAuthorityDto);

                MaintenanceAuthority = await _unitOfWork.Repository<MaintenanceAuthority>().Add(MaintenanceAuthority);

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
                response.Id = MaintenanceAuthority.MaintenanceAuthorityId;
            }

            return response;
        }
    }
}

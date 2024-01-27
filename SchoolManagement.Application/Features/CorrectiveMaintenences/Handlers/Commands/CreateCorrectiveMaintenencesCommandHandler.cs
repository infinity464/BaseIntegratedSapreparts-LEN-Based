using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.CorrectiveMaintenence.Validators;
using SchoolManagement.Application.Features.CorrectiveMaintenences.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.CorrectiveMaintenences.Handlers.Commands
{
    public class CreateCorrectiveMaintenenceCommandHandler : IRequestHandler<CreateCorrectiveMaintenenceCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCorrectiveMaintenenceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateCorrectiveMaintenenceCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateCorrectiveMaintenenceDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CorrectiveMaintenenceDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                /////// File Upload //////////
                ///
                string uniqueFileName = null;

                if (request.CorrectiveMaintenenceDto.Doc != null)
                {
                    var fileName = Path.GetFileName(request.CorrectiveMaintenenceDto.Doc.FileName);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    var a = Directory.GetCurrentDirectory();
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\corrective-maintenence", uniqueFileName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await request.CorrectiveMaintenenceDto.Doc.CopyToAsync(fileSteam);
                    }
                }
                var CorrectiveMaintenence = _mapper.Map<CorrectiveMaintenence>(request.CorrectiveMaintenenceDto);
                CorrectiveMaintenence.RefDocument = request.CorrectiveMaintenenceDto.RefDocument ?? "files/corrective-maintenence/" + uniqueFileName;
                CorrectiveMaintenence = await _unitOfWork.Repository<CorrectiveMaintenence>().Add(CorrectiveMaintenence);
                CorrectiveMaintenence.DateOfDefect = CorrectiveMaintenence.DateOfDefect.Value.AddDays(1.0);
                CorrectiveMaintenence.WorkOrderDate = CorrectiveMaintenence.WorkOrderDate.Value.AddDays(1.0);
                CorrectiveMaintenence.WorkStartDate = CorrectiveMaintenence.WorkStartDate.Value.AddDays(1.0);

                CorrectiveMaintenence.CompletedStatus = 0;
                await _unitOfWork.Save();
                 
                //Get issue register and update CorrectiveCompleteStatus
                var issueRegister = await _unitOfWork.Repository<IssueRegister>().Get((int)request.CorrectiveMaintenenceDto.IssueRegisterId);
                issueRegister.CorrectiveCompleteStatus = 1;

                await _unitOfWork.Repository<IssueRegister>().Update(issueRegister);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = CorrectiveMaintenence.CorrectiveMaintenenceId;
            }

            return response;
        }
    }
}

using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.MaintenancePlanning.Validators;
using SchoolManagement.Application.Features.MaintenancePlannings.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.MaintenancePlannings.Handlers.Commands
{
    public class CreateMaintenancePlanningCommandHandler : IRequestHandler<CreateMaintenancePlanningCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMaintenancePlanningCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateMaintenancePlanningCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateMaintenancePlanningDtoValidator();
            var validationResult = await validator.ValidateAsync(request.MaintenancePlanningDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else

            {
                /////// File Upload //////////

                string uniqueFileNameJobListDocument = null;
                string uniqueFileNameRequiredSpearsDoc = null;
                string uniqueFileNameRequiredToolsDoc = null;
                string uniqueFileNameRequiredConsumablesDoc = null;


                if (request.MaintenancePlanningDto.JobList != null)
                {

                    var fileName = Path.GetFileName(request.MaintenancePlanningDto.JobList.FileName);
                    uniqueFileNameJobListDocument = Guid.NewGuid().ToString() + "_" + fileName;
                    var a = Directory.GetCurrentDirectory();
                    //var filePath = Path.Combine(_hostingEnv.WebRootPath, "Content\\images\\profile", uniqueFileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\maintenance-planning", uniqueFileNameJobListDocument);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await request.MaintenancePlanningDto.JobList.CopyToAsync(fileSteam);
                    }
                }

                if (request.MaintenancePlanningDto.SpearsDoc != null)
                {
                    var fileName = Path.GetFileName(request.MaintenancePlanningDto.SpearsDoc.FileName);
                    uniqueFileNameRequiredSpearsDoc = Guid.NewGuid().ToString() + "_" + fileName;
                    var a = Directory.GetCurrentDirectory();
                    //var filePath = Path.Combine(_hostingEnv.WebRootPath, "Content\\images\\profile", uniqueFileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\maintenance-planning", uniqueFileNameRequiredSpearsDoc);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await request.MaintenancePlanningDto.SpearsDoc.CopyToAsync(fileSteam);
                    }
                }
                if (request.MaintenancePlanningDto.ToolsDoc != null)
                {
                    var fileName = Path.GetFileName(request.MaintenancePlanningDto.ToolsDoc.FileName);
                    uniqueFileNameRequiredToolsDoc = Guid.NewGuid().ToString() + "_" + fileName;
                    var a = Directory.GetCurrentDirectory();
                    //var filePath = Path.Combine(_hostingEnv.WebRootPath, "Content\\images\\profile", uniqueFileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\maintenance-planning", uniqueFileNameRequiredToolsDoc);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await request.MaintenancePlanningDto.ToolsDoc.CopyToAsync(fileSteam);
                    }
                }
                if (request.MaintenancePlanningDto.ConsumableDoc != null)
                {
                    var fileName = Path.GetFileName(request.MaintenancePlanningDto.ConsumableDoc.FileName);
                    uniqueFileNameRequiredConsumablesDoc = Guid.NewGuid().ToString() + "_" + fileName;
                    var a = Directory.GetCurrentDirectory();
                    //var filePath = Path.Combine(_hostingEnv.WebRootPath, "Content\\images\\profile", uniqueFileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\maintenance-planning", uniqueFileNameRequiredConsumablesDoc);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await request.MaintenancePlanningDto.ConsumableDoc.CopyToAsync(fileSteam);
                    }
                }
                var MaintenancePlanning = _mapper.Map<MaintenancePlanning>(request.MaintenancePlanningDto);
                MaintenancePlanning.JobListDocument = request.MaintenancePlanningDto.JobListDocument ?? "files/maintenance-planning/" + uniqueFileNameJobListDocument;
                MaintenancePlanning.RequiredSpearsDoc = request.MaintenancePlanningDto.RequiredSpearsDoc ?? "files/maintenance-planning/" + uniqueFileNameRequiredSpearsDoc;
                MaintenancePlanning.RequiredToolsDoc = request.MaintenancePlanningDto.RequiredToolsDoc ?? "files/maintenance-planning/" + uniqueFileNameRequiredToolsDoc;
                MaintenancePlanning.RequiredConsumablesDoc = request.MaintenancePlanningDto.RequiredConsumablesDoc ?? "files/maintenance-planning/" + uniqueFileNameRequiredConsumablesDoc;

                MaintenancePlanning = await _unitOfWork.Repository<MaintenancePlanning>().Add(MaintenancePlanning);
                MaintenancePlanning.LastInspDate = MaintenancePlanning.LastInspDate.Value.AddDays(1.0);
                MaintenancePlanning.NestInspDate = MaintenancePlanning.NestInspDate.Value.AddDays(1.0);

                await _unitOfWork.Save();
                

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = MaintenancePlanning.MaintenancePlanningId;
            }

            return response;
        }
    }
}

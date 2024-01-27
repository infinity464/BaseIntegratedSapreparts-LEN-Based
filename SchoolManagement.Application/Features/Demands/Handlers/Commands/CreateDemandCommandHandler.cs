using AutoMapper;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.Demands.Validators;
using SchoolManagement.Application.Features.Demands.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.Demands.Handlers.Commands
{
    public class CreateDemandCommandHandler : IRequestHandler<CreateDemandCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISchoolManagementRepository<IssueRegister> _issueRegisterRepository;

        public CreateDemandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISchoolManagementRepository<IssueRegister> issueRegisterRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _issueRegisterRepository = issueRegisterRepository;
        }

        public async Task<BaseCommandResponse> Handle(CreateDemandCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateDemandDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DemandDto);


            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {

                /////// File Upload //////////

                string uniqueFileName = null;


                if (request.DemandDto.Doc != null)
                {
                    var fileName = Path.GetFileName(request.DemandDto.Doc.FileName);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;
                    var a = Directory.GetCurrentDirectory();
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Content\\files\\demand", uniqueFileName);
                    using (var fileSteam = new FileStream(filePath, FileMode.Create))
                    {
                        await request.DemandDto.Doc.CopyToAsync(fileSteam);
                    }
                }

                if(request.DemandDto.IssueRegisterId != null)
                {
                    var IssueRegister = await _unitOfWork.Repository<IssueRegister>().Get((int)request.DemandDto.IssueRegisterId);

                    IssueRegister.DemandCompleteStatus = 1;

                    await _unitOfWork.Repository<IssueRegister>().Update(IssueRegister);
                    await _unitOfWork.Save();
                }

                if (request.DemandDto.SurveyId != null)
                {
                    var survey = await _unitOfWork.Repository<Survey>().Get((int)request.DemandDto.SurveyId);

                    survey.DemandCompleteStatus=1;

                    await _unitOfWork.Repository<Survey>().Update(survey);
                    await _unitOfWork.Save();
                }
                var demands = _mapper.Map<Demand>(request.DemandDto);
                demands.DemandCompleteStatus = 0;
               
                demands.SpecDoc = request.DemandDto.SpecDoc ?? "files/demand/" + uniqueFileName;
                demands.ProcurementStatus = 0;
                demands.StoreStatus = 0;
                demands.SftStatus = 0;
                demands.DemandCompleteStatus = 1;
                // var ReadingMaterials = _mapper.Map<ReadingMaterial>(request.ReadingMaterialDto);

                
                    demands = await _unitOfWork.Repository<Demand>().Add(demands);
                    demands.DemandDate = demands.DemandDate.Value.AddDays(1.0);

                try
                {
                    await _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }





                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = demands.DemandId;
            }

            return response;
        }
    }
}

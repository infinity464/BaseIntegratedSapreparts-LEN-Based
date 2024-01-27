using AutoMapper;
using FluentValidation;
using MediatR;
using SchoolManagement.Application.Contracts.Persistence;
using SchoolManagement.Application.DTOs.IssueRegister.Validators;
using SchoolManagement.Application.Features.IssueRegisters.Requests.Commands;
using SchoolManagement.Application.Responses;
using SchoolManagement.Domain;

namespace SchoolManagement.Application.Features.IssueRegisters.Handlers.Commands
{
    public class CreateIssueRegisterCommandHandler : IRequestHandler<CreateIssueRegisterCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateIssueRegisterCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateIssueRegisterCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateIssueRegisterDtoValidator();
            var validationResult = await validator.ValidateAsync(request.IssueRegisterDto);

            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var issueRegister = request.IssueRegisterDto;

                //For Update ItemStor table
                //var itemStoreId = issueRegister.ItemStoreList.Select(x => x.ItemStorId.Value).FirstOrDefault();
                //var itemStore= await _unitOfWork.Repository<ItemStor>().Get(itemStoreId);


                var issueRegisterList = issueRegister.ItemStoreList.Select(x => new IssueRegister()
                {
                    IssueRegisterId = issueRegister.IssueRegisterId.Value,
                    ItemStoreId = x.ItemStorId,
                    UnitId = issueRegister.UnitId,
                    ItemCategoryId = issueRegister.ItemCategoryId,
                    ReturnQty = 0,
                    EquipmentNameId = issueRegister.EquipmentNameId,
                    ToolsLocationId = x.ToolsLocationId,
                    IssuedBy = issueRegister.IssuedBy,
                    DepartmentNameId = x.DepartmentNameId,
                    SparesCategoryId = x.SparesCategoryId,
                    ItemDetailId = x.ItemDetailId,
                    TrainingCrewId = issueRegister.TrainingCrewId,
                    IssueStatusId = issueRegister.IssueStatusId,
                    TotalReceivedQty = x.TotalReceivedQty,
                    IssueTypeId = x.IssueTypeId,
                    IssueQty = x.IssuedQty,
                    IssueDate = issueRegister.IssueDate,
                    IssuedTo = issueRegister.IssuedTo,
                    Reason = issueRegister.Reason,
                    IsRefundable = x.IsRefundable,
                    AvailableQtyBeforeIssue = issueRegister.AvailableQtyBeforeIssue,
                    AvailableQtyAfterIssue = 0,
                    ReceivedPerson = issueRegister.ReceivedPerson,
                    Remarks = issueRegister.Remarks,
                    IsActive = issueRegister.IsActive,
                    CorrectiveCompleteStatus = 0,
                    PreventiveCompleteStatus = 0,
                    ScheduleCompleteStatus = 0,
                    SurveyStatus = 0,
                    Status = issueRegister.Status,
                    DemandCompleteStatus = issueRegister.DemandCompleteStatus
                });

                //For Update Item StorList and minus issue qty

                foreach (var item in issueRegisterList)
                {
                    var itemStor = await _unitOfWork.Repository<ItemStor>().Get(item.ItemStoreId.Value);
                    var availableQty = itemStor.AvailableQty;
                    var issueQty = item.IssueQty;

                    itemStor.AvailableQty = availableQty - issueQty;
                    await _unitOfWork.Repository<ItemStor>().Update(itemStor);
                    await _unitOfWork.Save();
                }

                  foreach (var item in issueRegisterList)
                {
                    var itemStor = await _unitOfWork.Repository<ItemStor>().Get(item.ItemStoreId.Value);

                    //parmanent
                    if (item.IssueTypeId == 1)
                    {
                        itemStor.PermanentQty += item.IssueQty;
                    }

                    //ty
                    if (item.IssueTypeId == 2)
                    {
                        itemStor.TyQty += item.IssueQty;
                    }
                    //survey
                    if (item.IssueTypeId == 3)
                    {
                        itemStor.SurveyQty += item.IssueQty;
                    }
                    //repair
                    if (item.IssueTypeId == 4)
                    {
                        itemStor.RepairQty += item.IssueQty;
                    }
                    //shift fitted 
                    if (item.IssueTypeId == 8)
                    {
                        itemStor.ShipFittedQty += item.IssueQty;
                    }

                    //    var availableQty = itemStor.AvailableQty;
                    //var issueQty = item.IssueQty;

                    //itemStor.AvailableQty = availableQty - issueQty;
                    await _unitOfWork.Repository<ItemStor>().Update(itemStor);
                    await _unitOfWork.Save();
                }


                //For Update ItemStor table and minus issue qty
                //var issueqty = issueRegisterList.Select(x => x.IssueQty).FirstOrDefault();
                //itemStore.AvailableQty = itemStore.AvailableQty - issueqty;
                //await _unitOfWork.Repository<ItemStor>().Update(itemStore);
                //await _unitOfWork.Save();

                await _unitOfWork.Repository<IssueRegister>().AddRangeAsync(issueRegisterList);

                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
            }

            return response;
        }
    }
}

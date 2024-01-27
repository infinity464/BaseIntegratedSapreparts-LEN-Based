using AutoMapper;
using SchoolManagement.Domain;
using SchoolManagement.Application.DTOs.MaintenanceCategory;
using SchoolManagement.Application.DTOs.AccountType;

using SchoolManagement.Application.DTOs.GseMaintenance;
using SchoolManagement.Application.DTOs.GseScheduleWorkType;
using SchoolManagement.Application.DTOs.GseMaintenanceScheduleName;
using SchoolManagement.Application.DTOs.GseItemName;
using SchoolManagement.Application.DTOs.LifeLimitItem;
using SchoolManagement.Application.DTOs.LifeLimitItemRunningHour;
using SchoolManagement.Application.DTOs.ReminderType;
using SchoolManagement.Application.DTOs.Trade;

using SchoolManagement.Application.DTOs.FiscalYears;
using SchoolManagement.Application.DTOs.Denos;
using SchoolManagement.Application.DTOs.ItemTypes;
using SchoolManagement.Application.DTOs.ItemStatuses;
using SchoolManagement.Application.DTOs.Suppliers;
using SchoolManagement.Application.DTOs.ConditionOfItems;
using SchoolManagement.Application.DTOs.EndLifeTypes;
using SchoolManagement.Application.DTOs.ServiceLifeTypes;
using SchoolManagement.Application.DTOs.ItemCategorys;
using SchoolManagement.Application.DTOs.ToolsTypes;
using SchoolManagement.Application.DTOs.SparesCategorys;
using SchoolManagement.Application.DTOs.AcctStores;
using SchoolManagement.Application.DTOs.AcStatus;
using SchoolManagement.Application.DTOs.Courses;
using SchoolManagement.Application.DTOs.Coursees;
using SchoolManagement.Application.DTOs.DemandAuthority;
using SchoolManagement.Application.DTOs.ShelfLifeCategory;
using SchoolManagement.Application.DTOs.Store;
using SchoolManagement.Application.DTOs.Rank;
using SchoolManagement.Application.DTOs.OccasionOfDemand;
using SchoolManagement.Application.DTOs.Authority;
using SchoolManagement.Application.DTOs.DepartmentName;
using SchoolManagement.Application.DTOs.AirCraftName;
using SchoolManagement.Application.DTOs.RunningHour;
using SchoolManagement.Application.DTOs.MaintenanceSubCategory;
using SchoolManagement.Application.DTOs.MaintenanceType;
using SchoolManagement.Application.DTOs.DemandCompleteStatuses;
using SchoolManagement.Application.DTOs.DemandDocs;
using SchoolManagement.Application.DTOs.Acceptances;

using SchoolManagement.Application.DTOs.SourceOfSupply;
using SchoolManagement.Application.DTOs.RetirementType;
using SchoolManagement.Application.DTOs.ProcurementStatus;
using SchoolManagement.Application.DTOs.PrincipalName;
using SchoolManagement.Application.DTOs.PlaceOfDelivery;
using SchoolManagement.Application.DTOs.PartOfShipment;
using SchoolManagement.Application.DTOs.OverhaulingType;
using SchoolManagement.Application.DTOs.OfficersStatus;
using SchoolManagement.Application.DTOs.Manufacture;
using SchoolManagement.Application.DTOs.LocalAgent;
using SchoolManagement.Application.DTOs.ItemCategoryType;
using SchoolManagement.Application.DTOs.DemandType;
using SchoolManagement.Application.DTOs.Demands;
using SchoolManagement.Application.DTOs.ItemDetail;
using SchoolManagement.Application.DTOs.Procurement;

using SchoolManagement.Application.DTOs.ItemInspection;

using SchoolManagement.Application.DTOs.CstTec;
using SchoolManagement.Application.DTOs.ItemStor;
using SchoolManagement.Application.DTOs.IssueStatus;
using SchoolManagement.Application.DTOs.IssueRegister;
using SchoolManagement.Application.DTOs.MaintenancePlanning;
using SchoolManagement.Application.DTOs.TrainingCrew;
using SchoolManagement.Application.DTOs.Features;
using SchoolManagement.Application.DTOs.Modules;
using SchoolManagement.Application.DTOs.Role;
using SchoolManagement.Application.DTOs.RoleFeature;
using SchoolManagement.Application.DTOs.User;
using SchoolManagement.Application.DTOs.Caste;
using SchoolManagement.Application.DTOs.District;
using SchoolManagement.Application.DTOs.Division;
using SchoolManagement.Application.DTOs.Religion;
using SchoolManagement.Application.DTOs.Thana;
using SchoolManagement.Application.DTOs.CodeValues;
using SchoolManagement.Application.DTOs.CodeValueType;
using SchoolManagement.Application.DTOs.IssueRegister.MultipleInsertDto;
using SchoolManagement.Application.DTOs.MaintenancePlanningStatus;
using SchoolManagement.Application.Helpers;
using SchoolManagement.Application.DTOs.ToolsLocation;
using SchoolManagement.Application.DTOs.ToolsBoxNames;
using SchoolManagement.Application.DTOs.AirCraftFlying;
using SchoolManagement.Application.DTOs.EquipmentName;
using SchoolManagement.Application.DTOs.MaintenanceSchedule;
using SchoolManagement.Application.DTOs.PreviousItemStore;
using SchoolManagement.Application.DTOs.PresentState;
using SchoolManagement.Application.DTOs.MeaSquadronState;
using SchoolManagement.Application.DTOs.CallibrationState;

using SchoolManagement.Application.DTOs.BaseSchoolNames;
using SchoolManagement.Application.DTOs.Group;
using SchoolManagement.Application.DTOs.ProcurementType;
using SchoolManagement.Application.DTOs.Opinion;
using SchoolManagement.Application.DTOs.IssueType;
using SchoolManagement.Application.DTOs.ByWhom;
using SchoolManagement.Application.DTOs.MaintenanceAuthority;
using SchoolManagement.Application.DTOs.Priority;
using SchoolManagement.Application.DTOs.StatusOfJob;
using SchoolManagement.Application.DTOs.Progres;
using SchoolManagement.Application.DTOs.JobCategory;
using SchoolManagement.Application.DTOs.JobDescription;
using SchoolManagement.Application.DTOs.ReturnStatus;
using SchoolManagement.Application.DTOs.ReturnLogs;
using SchoolManagement.Application.DTOs.SurveyItem;
using SchoolManagement.Application.DTOs.SurveyItemType;
using SchoolManagement.Application.DTOs.SurveyAuthority;
using SchoolManagement.Application.DTOs.PreventiveMaintenance;
using SchoolManagement.Application.DTOs.CorrectiveMaintenence;
using SchoolManagement.Application.DTOs.PmsNumberEntry;
using SchoolManagement.Application.DTOs.JobDescriptionEntryPms;
using SchoolManagement.Application.DTOs.SpareRequiredEntry;
using SchoolManagement.Application.DTOs.JobCategoryMaintenences;
using SchoolManagement.Application.DTOs.ScheduleMaintenenceTypes;
using SchoolManagement.Application.DTOs.JobDescriptionScheduleMaintenences;
using SchoolManagement.Application.DTOs.ScheduleMaintenances;
using SchoolManagement.Application.DTOs.SpareRequisition;
using SchoolManagement.Application.DTOs.SpareRequiredStatus;
using SchoolManagement.Application.DTOs.Survey;
using SchoolManagement.Application.DTOs.UserManual;

namespace SchoolManagement.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {


            //Lattar A


            #region MaintenanceCategory Mapping    
            CreateMap<MaintenanceCategoryDto, MaintenanceCategory>().ReverseMap()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName))
                .ForMember(d => d.MaintenanceType, o => o.MapFrom(s => s.MaintenanceType.Name));
            CreateMap<MaintenanceCategory, CreateMaintenanceCategoryDto>().ReverseMap();
            #endregion

            #region GseMaintenance Mapping    
            CreateMap<GseMaintenanceDto,GseMaintenance>().ReverseMap()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName))
                .ForMember(d => d.GseItemName, o => o.MapFrom(s => s.GseItemName.ItemName))
                .ForMember(d => d.GseMaintenanceScheduleName, o => o.MapFrom(s => s.GseMaintenanceScheduleName.ScheduleName))
                .ForMember(d => d.GseScheduleWorkType, o => o.MapFrom(s => s.GseScheduleWorkType.ScheduleWorkName));
            CreateMap<GseMaintenance, CreateGseMaintenanceDto>().ReverseMap();
            #endregion

            #region GseScheduleWorkType Mapping    
            CreateMap<GseScheduleWorkTypeDto, GseScheduleWorkType>().ReverseMap()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName))
                .ForMember(d => d.GseMaintenanceScheduleName, o => o.MapFrom(s => s.GseMaintenanceScheduleName.ScheduleName));
            CreateMap<GseScheduleWorkType, CreateGseScheduleWorkTypeDto>().ReverseMap();
            #endregion

            #region GseMaintenanceScheduleName Mapping    
            CreateMap<GseMaintenanceScheduleNameDto, GseMaintenanceScheduleName>().ReverseMap()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName));
            CreateMap<GseMaintenanceScheduleName, CreateGseMaintenanceScheduleNameDto>().ReverseMap();
            #endregion

            #region GseItemName Mapping    
            CreateMap<GseItemNameDto,GseItemName>().ReverseMap()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName));
            CreateMap<GseItemName, CreateGseItemNameDto>().ReverseMap();
            #endregion

            #region LifeLimitItem Mapping    
            CreateMap<LifeLimitItem, LifeLimitItemDto>().ReverseMap();
            CreateMap<LifeLimitItem, CreateLifeLimitItemDto>().ReverseMap();
            #endregion

            #region CstTec Mapping    
            CreateMap<CstTec, CstTecDto>().ReverseMap();
            CreateMap<CstTec, CreateCstTecDto>().ReverseMap();
            #endregion

            #region LifeLimitItemRunningHour Mapping    
            CreateMap<LifeLimitItemRunningHourDto,LifeLimitItemRunningHour>().ReverseMap()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName))
                .ForMember(d => d.LifeLimitItem, o => o.MapFrom(s => s.LifeLimitItem.Name))
                .ForMember(d => d.MaintenanceCategory, o => o.MapFrom(s => s.MaintenanceCategory.CategoryName));
            CreateMap<LifeLimitItemRunningHour, CreateLifeLimitItemRunningHourDto>().ReverseMap();
            #endregion

            #region ReminderType Mapping    
            CreateMap<ReminderType, ReminderTypeDto>().ReverseMap();
            CreateMap<ReminderType, CreateReminderTypeDto>().ReverseMap();
            #endregion

            #region CorrectiveMaintenence Mapping    
            CreateMap<CorrectiveMaintenenceDto, CorrectiveMaintenence>().ReverseMap()
                 .ForMember(d => d.StatusOfJob, o => o.MapFrom(s => s.StatusOfJob.Name))
                 .ForMember(d => d.EquipmentName, o => o.MapFrom(s => s.EquipmentName.Name))
                 .ForMember(d => d.PartNo, o => o.MapFrom(s => s.ItemDetail.PartNo))
                 .ForMember(d => d.ItemName, o => o.MapFrom(s => s.ItemDetail.NameOfItem))
                 .ForMember(d => d.ItemName, o => o.MapFrom(s => s.ItemDetail.NameOfItem))
                 .ForMember(d => d.RefDocument, o => o.MapFrom<CorrectiveMaintenenceFileUrlResolver>());
            CreateMap<CorrectiveMaintenence, CreateCorrectiveMaintenenceDto>().ReverseMap();
            #endregion

            #region Trade Mapping    
            CreateMap<Trade, TradeDto>().ReverseMap();
            CreateMap<Trade, CreateTradeDto>().ReverseMap();
            #endregion

            #region IssueType Mapping    
            CreateMap<IssueType, IssueTypeDto>().ReverseMap();
            CreateMap<IssueType, CreateIssueTypeDto>().ReverseMap();
            #endregion

            #region ProcurementType Mapping    
            CreateMap<ProcurementType, ProcurementTypeDto>().ReverseMap();
            CreateMap<ProcurementType, CreateProcurementTypeDto>().ReverseMap();
            #endregion

            #region Opinion Mapping    
            CreateMap<Opinion, OpinionDto>().ReverseMap();
            CreateMap<Opinion, CreateOpinionDto>().ReverseMap();
            #endregion

            #region Survey Mapping    
            CreateMap<Survey, SurveyDto>().ReverseMap();
            CreateMap<Survey, CreateSurveyDto>().ReverseMap();
            #endregion

            #region MaintenanceAuthority Mapping    
            CreateMap<MaintenanceAuthority, MaintenanceAuthorityDto>().ReverseMap();
            CreateMap<MaintenanceAuthority, CreateMaintenanceAuthorityDto>().ReverseMap();
            #endregion

            #region Priority Mapping    
            CreateMap<Priority, PriorityDto>().ReverseMap();
            CreateMap<Priority, CreatePriorityDto>().ReverseMap();
            #endregion

            #region StatusOfJob Mapping    
            CreateMap<StatusOfJob, StatusOfJobDto>().ReverseMap();
            CreateMap<StatusOfJob, CreateStatusOfJobDto>().ReverseMap();
            #endregion

            #region ByWhom Mapping    
            CreateMap<ByWhom, ByWhomDto>().ReverseMap();
            CreateMap<ByWhom, CreateByWhomDto>().ReverseMap();
            #endregion

            #region Procurement Mapping    
            CreateMap<ProcurementDto, Procurement>().ReverseMap()
                .ForMember(d => d.CstTec, o => o.MapFrom(s => s.CstTec.Name))
                .ForMember(d => d.Supplier, o => o.MapFrom(s => s.Supplier.CompanyName))
                .ForMember(d => d.ItemDetail, o => o.MapFrom(s => s.ItemDetail.PartNo))
                .ForMember(d => d.Deno, o => o.MapFrom(s => s.Demand.Deno.Name))
                .ForMember(d => d.ProcurementStatusForStatusDropdown, o => o.MapFrom(s => s.ProcurementStatus.Name))
                .ForMember(d => d.Demand, o => o.MapFrom(s => s.Demand.DemandNo))
                .ForMember(d => d.ItemName, o => o.MapFrom(s => s.ItemDetail.NameOfItem))
                .ForMember(d => d.ProcurementDocument, o => o.MapFrom<ProcurementFileUrlResolver>());
            CreateMap<Procurement, CreateProcurementDto>().ReverseMap();
            #endregion


            #region Features Mapping    
            CreateMap<FeatureDto, Feature>().ReverseMap()
             .ForMember(d => d.ModuleName, o => o.MapFrom(s => s.Module.ModuleName));

            CreateMap<Feature, CreateFeatureDto>().ReverseMap();
            #endregion

            #region Modules Mapping    
            CreateMap<Module, ModuleDto>().ReverseMap();
            CreateMap<Module, ModuleFeatureDto>().ReverseMap();

            CreateMap<Module, CreateModuleDto>().ReverseMap();
            #endregion 

            #region Role Mappings 
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Role, CreateRoleDto>().ReverseMap();
            #endregion

            #region ReturnStatus Mappings 
            CreateMap<ReturnStatus, ReturnStatusDto>().ReverseMap();
            CreateMap<ReturnStatus, CreateReturnStatusDto>().ReverseMap();
            #endregion

            #region ReturnLog Mappings 
            CreateMap<ReturnLog, ReturnLogDto>().ReverseMap();
            //CreateMap<CreateReturnLogDto, ReturnLog>().ReverseMap();

            CreateMap<ReturnLog, AddReturnLogToStoreDto>().ReverseMap()
                .ForMember(d => d.ReceiveQty, o => o.MapFrom(s => s.ReturnQtyUpdate));
            CreateMap<ReturnLog, CreateReturnLogDto>().ReverseMap();
            #endregion

            #region RoleFeature Mappings 
            CreateMap<RoleFeature, RoleFeatureDto>().ReverseMap();
            CreateMap<RoleFeature, CreateRoleFeatureDto>().ReverseMap();
            #endregion

            //#region User Mapping    
            //CreateMap<UserDto, User>().ReverseMap()
            //  .ForMember(d => d.Role, o => o.MapFrom(s => s.Role.RoleName))
            //  .ForMember(d => d.BranchInfo, o => o.MapFrom(s => s.BranchInfo.BranchName));
            //CreateMap<User, CreateUserDto>().ReverseMap();
            //#endregion

            #region Caste Mappings
            CreateMap<CasteDto, Caste>().ReverseMap()
              .ForMember(d => d.Religion, o => o.MapFrom(s => s.Religion.ReligionName));

            CreateMap<Caste, CreateCasteDto>().ReverseMap();
            #endregion

            #region Religion Mappings
            CreateMap<Religion, ReligionDto>().ReverseMap();
            CreateMap<Religion, CreateReligionDto>().ReverseMap();
            #endregion


            #region Division Mappings
            CreateMap<Division, DivisionDto>().ReverseMap();
            CreateMap<Division, CreateDivisionDto>().ReverseMap();
            #endregion


            #region BaseSchoolNames Mapping   
            CreateMap<BaseSchoolName, BaseSchoolNameDto>().ReverseMap();
            CreateMap<BaseSchoolName, CreateBaseSchoolNameDto>().ReverseMap();
            #endregion  

            #region District Mappings
            CreateMap<DistrictDto, District>().ReverseMap()
              .ForMember(d => d.Division, o => o.MapFrom(s => s.Division.DivisionName));
            CreateMap<District, CreateDistrictDto>().ReverseMap();
            #endregion

            #region Thana Mappings
            CreateMap<ThanaDto, Thana>().ReverseMap()
              .ForMember(d => d.District, o => o.MapFrom(s => s.District.DistrictName));
            CreateMap<Thana, CreateThanaDto>().ReverseMap();
            #endregion

            #region CodeValues  Mappings 
            CreateMap<CodeValueDto, CodeValue>().ReverseMap()
              .ForMember(d => d.CodeValueType, o => o.MapFrom(s => s.CodeValueType.Type));

            CreateMap<CodeValue, CreateCodeValueDto>().ReverseMap();
            #endregion

            #region CodeValueType  Mappings 
            CreateMap<CodeValueType, CodeValueTypeDto>().ReverseMap();
            CreateMap<CodeValueType, CreateCodeValueTypeDto>().ReverseMap();
            #endregion

            //#region Procurement Mapping    
            //CreateMap<ProcurementDto, Procurement>().ReverseMap()
            //    .ForMember(d => d.Demand, o => o.MapFrom(s => s.Demand.RefPoNo))
            //    .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.Name))
            //    .ForMember(d => d.ItemDetail, o => o.MapFrom(s => s.ItemDetail.PartNo))
            //    .ForMember(d => d.LocalAgent, o => o.MapFrom(s => s.LocalAgent.Name))
            //    .ForMember(d => d.PartOfShipment, o => o.MapFrom(s => s.PartOfShipment.Name))
            //    .ForMember(d => d.PrincipalName, o => o.MapFrom(s => s.PrincipalName.Name))
            //    .ForMember(d => d.ProcurementStatus, o => o.MapFrom(s => s.ProcurementStatus.Name));
            //CreateMap<Procurement, CreateProcurementDto>().ReverseMap();
            //#endregion



            #region AccountType Mapping    
            CreateMap<AccountType, AccountTypeDto>().ReverseMap();
            CreateMap<AccountType, CreateAccountTypeDto>().ReverseMap();
            #endregion

            //#region CallibrationState Mapping    
            //CreateMap<CallibrationState, CallibrationStateDto>().ReverseMap();
            //   // .ForMember(d => d.Trade, o => o.MapFrom(s => s.Trade.Name));
            //CreateMap<CallibrationState, CreateCallibrationStateDto>().ReverseMap();
            //#endregion

            #region CallibrationState Mapping    
            CreateMap<CallibrationStateDto, CallibrationState>().ReverseMap()
                .ForMember(d => d.Trade, o => o.MapFrom(s => s.Trade.Name));
            CreateMap<CallibrationState, CreateCallibrationStateDto>().ReverseMap();
            #endregion

            #region FiscalYear Mappings 
            CreateMap<FiscalYear, FiscalYearDto>().ReverseMap();
            CreateMap<FiscalYear, CreateFiscalYearDto>().ReverseMap();
            #endregion

            #region Deno Mappings 
            CreateMap<Deno, DenoDto>().ReverseMap();
            CreateMap<Deno, CreateDenoDto>().ReverseMap();
            #endregion
            #region EquipmentName Mappings 
            CreateMap<EquipmentNameDto, EquipmentName>().ReverseMap()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName))
                .ForMember(d => d.ItemCategory, o => o.MapFrom(s => s.ItemCategory.Name));
            CreateMap<EquipmentName, CreateEquipmentNameDto>().ReverseMap();
            #endregion

            #region ItemType Mappings 
            CreateMap<ItemType, ItemTypeDto>().ReverseMap();
            CreateMap<ItemType, CreateItemTypeDto>().ReverseMap();
            #endregion

            #region Progres Mappings 
            CreateMap<Progres, ProgresDto>().ReverseMap();
            CreateMap<Progres, CreateProgresDto>().ReverseMap();
            #endregion

            #region JobCategory Mappings 
            CreateMap<JobCategory, JobCategoryDto>().ReverseMap();
            CreateMap<JobCategory, CreateJobCategoryDto>().ReverseMap();
            #endregion

            #region JobDescription Mappings 
            CreateMap<JobDescription, JobDescriptionDto>().ReverseMap();
            CreateMap<JobDescription, CreateJobDescriptionDto>().ReverseMap();
            #endregion


            #region ItemStatus Mappings 
            CreateMap<ItemStatus, ItemStatusDto>().ReverseMap();
            CreateMap<ItemStatus, CreateItemStatusDto>().ReverseMap();
            #endregion

            #region ShelfLifeCategory Mapping    
            CreateMap<ShelfLifeCategory, ShelfLifeCategoryDto>().ReverseMap();
            CreateMap<ShelfLifeCategory, CreateShelfLifeCategoryDto>().ReverseMap();
            #endregion

            #region Store Mapping    
            CreateMap<Store, StoreDto>().ReverseMap();
            CreateMap<Store, CreateStoreDto>().ReverseMap();
            #endregion

            #region Rank Mapping    
            CreateMap<Rank, RankDto>().ReverseMap();
            CreateMap<Rank, CreateRankDto>().ReverseMap();
            #endregion


            #region OccasionOfDemand Mapping    
            CreateMap<OccasionOfDemandDto, OccasionOfDemand>().ReverseMap()
                .ForMember(d => d.FiscalYear, o => o.MapFrom(s => s.FiscalYear.FiscalYearName));
            CreateMap<OccasionOfDemand, CreateOccasionOfDemandDto>().ReverseMap();
            #endregion

            #region Authority Mapping    
            CreateMap<Authority, AuthorityDto>().ReverseMap();
            CreateMap<Authority, CreateAuthorityDto>().ReverseMap();
            #endregion

            #region DepartmentName Mapping    
            CreateMap<DepartmentName, DepartmentNameDto>().ReverseMap();
            CreateMap<DepartmentName, CreateDepartmentNameDto>().ReverseMap();
            #endregion

            #region AirCraftName Mapping    
            CreateMap<AirCraftNameDto, AirCraftName>().ReverseMap()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName))
                .ForMember(d => d.Image, o => o.MapFrom<PhotoUrlResolver>());
            CreateMap<AirCraftName, CreateAirCraftNameDto>().ReverseMap();
            #endregion

            #region AirCraftFlying Mapping    
            CreateMap<AirCraftFlyingDto, AirCraftFlying>().ReverseMap()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName))
                .ForMember(d => d.AirCraftName, o => o.MapFrom(s => s.AirCraftName.Name));
            CreateMap<AirCraftFlying, CreateAirCraftFlyingDto>().ReverseMap();
            #endregion

            #region RunningHour Mapping    
            CreateMap<RunningHourDto, RunningHour>().ReverseMap()
                .ForMember(d => d.AirCraftName, o => o.MapFrom(s => s.AirCraftName.Name))
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName));
            CreateMap<RunningHour, CreateRunningHourDto>().ReverseMap();
            #endregion

            #region MaintenanceSubCategory Mapping    
            CreateMap<MaintenanceSubCategoryDto, MaintenanceSubCategory>().ReverseMap()
                .ForMember(d => d.MaintenanceCategory, o => o.MapFrom(s => s.MaintenanceCategory.CategoryName))
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName));
            CreateMap<MaintenanceSubCategory, CreateMaintenanceSubCategoryDto>().ReverseMap();
            #endregion

            #region MaintenanceType Mapping    
            CreateMap<MaintenanceTypeDto, MaintenanceType>().ReverseMap()
                 .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName));
            CreateMap<MaintenanceType, CreateMaintenanceTypeDto>().ReverseMap();
            #endregion


            #region SourceOfSupply Mapping    
            CreateMap<SourceOfSupply, SourceOfSupplyDto>().ReverseMap();
            CreateMap<SourceOfSupply, CreateSourceOfSupplyDto>().ReverseMap();
            #endregion

            #region ScheduleMaintenenceType Mappings 
            CreateMap<ScheduleMaintenenceType, ScheduleMaintenenceTypeDto>().ReverseMap();
            CreateMap<ScheduleMaintenenceType, CreateScheduleMaintenenceTypeDto>().ReverseMap();
            #endregion

            #region SpareRequisition Mappings 
            CreateMap<SpareRequisition, SpareRequisitionDto>().ReverseMap();
            CreateMap<SpareRequisition, CreateSpareRequisitionDto>().ReverseMap();
            #endregion

            #region SpareRequiredStatus Mappings 
            CreateMap<SpareRequiredStatus, SpareRequiredStatusDto>().ReverseMap();
            CreateMap<SpareRequiredStatus, CreateSpareRequiredStatusDto>().ReverseMap();
            #endregion


            #region Job Description Schedule Maintenence Mappings 
            CreateMap<JobDescriptionScheduleMaintenenceDto, JobDescriptionScheduleMaintenence>().ReverseMap()
               .ForMember(d => d.ScheduleMaintenenceType, o => o.MapFrom(s => s.ScheduleMaintenenceType.Name))
               .ForMember(d => d.JobCategoryMaintenence, o => o.MapFrom(s => s.JobCategoryMaintenence.CategoryName))
               .ForMember(d => d.ItemName, o => o.MapFrom(s => s.ItemDetail.NameOfItem));
            CreateMap<JobDescriptionScheduleMaintenence, CreateJobDescriptionScheduleMaintenenceDto>().ReverseMap();
             
            //CreateMap<JobDescriptionScheduleMaintenence, JobDescriptionScheduleMaintenenceDto>().ReverseMap();
            //CreateMap<JobDescriptionScheduleMaintenence, CreateJobDescriptionScheduleMaintenenceDto>().ReverseMap();
            #endregion

            #region RetirementType Mapping    
            CreateMap<RetirementType, RetirementTypeDto>().ReverseMap();
            CreateMap<RetirementType, CreateRetirementTypeDto>().ReverseMap();
            #endregion

            #region ProcurementStatus Mapping    
            CreateMap<ProcurementStatus, ProcurementStatusDto>().ReverseMap();
            CreateMap<ProcurementStatus, CreateProcurementStatusDto>().ReverseMap();
            #endregion

            #region PrincipalName Mapping    
            CreateMap<PrincipalName, PrincipalNameDto>().ReverseMap();
            CreateMap<PrincipalName, CreatePrincipalNameDto>().ReverseMap();
            #endregion

            #region PresentState Mapping    
            CreateMap<PresentState, PresentStateDto>().ReverseMap();
            CreateMap<PresentState, CreatePresentStateDto>().ReverseMap();
            #endregion

            #region PreviousItemStore Mapping    
            CreateMap<PreviousItemStoreDto, PreviousItemStore>().ReverseMap()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName))
                .ForMember(d => d.ItemDetail, o => o.MapFrom(s => s.ItemDetail.PartNo))
                .ForMember(d => d.Deno, o => o.MapFrom(s => s.Deno.Name))
                .ForMember(d => d.ItemCategory, o => o.MapFrom(s => s.ItemCategory.Name))
                .ForMember(d => d.AcctStore, o => o.MapFrom(s => s.AcctStore.Name));
            CreateMap<PreviousItemStore, CreatePreviousItemStoreDto>().ReverseMap();
            #endregion

            #region PlaceOfDelivery Mapping    
            CreateMap<PlaceOfDelivery, PlaceOfDeliveryDto>().ReverseMap();
            CreateMap<PlaceOfDelivery, CreatePlaceOfDeliveryDto>().ReverseMap();
            #endregion

            #region PartOfShipment Mapping    
            CreateMap<PartOfShipment, PartOfShipmentDto>().ReverseMap();
            CreateMap<PartOfShipment, CreatePartOfShipmentDto>().ReverseMap();
            #endregion

            #region OverhaulingType Mapping    
            CreateMap<OverhaulingType, OverhaulingTypeDto>().ReverseMap();
            CreateMap<OverhaulingType, CreateOverhaulingTypeDto>().ReverseMap();
            #endregion


            #region OfficersStatus Mapping    
            CreateMap<OfficersStatus, OfficersStatusDto>().ReverseMap();
            CreateMap<OfficersStatus, CreateOfficersStatusDto>().ReverseMap();
            #endregion


            #region Supplier Mappings 
            CreateMap<Supplier, SupplierDto>().ReverseMap();
            CreateMap<Supplier, CreateSupplierDto>().ReverseMap();
            #endregion


            #region ConditionOfItem Mappings 
            CreateMap<ConditionOfItem, ConditionOfItemDto>().ReverseMap();
            CreateMap<ConditionOfItem, CreateConditionOfItemDto>().ReverseMap();
            #endregion 

            #region EndLifeType Mappings 
            CreateMap<EndLifeType, EndLifeTypeDto>().ReverseMap();
            CreateMap<EndLifeType, CreateEndLifeTypeDto>().ReverseMap();
            #endregion

            #region ServiceLifeType Mappings 
            CreateMap<ServiceLifeType, ServiceLifeTypeDto>().ReverseMap();
            CreateMap<ServiceLifeType, CreateServiceLifeTypeDto>().ReverseMap();
            #endregion

            #region Survey Mappings 
            CreateMap<SurveyItem, SurveyItemDto>().ReverseMap();
            CreateMap<SurveyItem, CreateSurveyItemDto>().ReverseMap();
            #endregion 

            #region ItemCategory Mappings  
            CreateMap<ItemCategory, ItemCategoryDto>().ReverseMap();
            CreateMap<ItemCategory, CreateItemCategoryDto>().ReverseMap();
            #endregion

            #region ToolsType Mappings 
            CreateMap<ToolsType, ToolsTypeDto>().ReverseMap();
            CreateMap<ToolsType, CreateToolsTypeDto>().ReverseMap();
            #endregion

            #region SparesCategory Mappings  
            CreateMap<SparesCategory, SparesCategoryDto>().ReverseMap();
            CreateMap<SparesCategory, CreateSparesCategoryDto>().ReverseMap();
            #endregion

            #region AcctStore Mappings  
            CreateMap<AcctStore, AcctStoreDto>().ReverseMap();
            CreateMap<AcctStore, CreateAcctStoreDto>().ReverseMap();
            #endregion

            #region AcctStatus Mappings   
            CreateMap<AcStatus, AcStatusDto>().ReverseMap();
            CreateMap<AcStatus, CreateAcStatusDto>().ReverseMap();
            #endregion

            #region Course Mappings   
            CreateMap<Course, CourseDto>().ReverseMap();
            CreateMap<Course, CreateCourseDto>().ReverseMap();
            #endregion

            #region DemandAuthority Mappings   
            CreateMap<DemandAuthority, DemandAuthorityDto>().ReverseMap();
            CreateMap<DemandAuthority, CreateDemandAuthorityDto>().ReverseMap();
            #endregion

            #region Group Mappings   
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<Group, CreateGroupDto>().ReverseMap();
            #endregion

            #region DemandCompleteStatus Mappings   
            CreateMap<DemandCompleteStatus, DemandCompleteStatusDto>().ReverseMap();
            CreateMap<DemandCompleteStatus, CreateDemandCompleteStatusDto>().ReverseMap();
            #endregion

            #region DemandDoc Mappings   
            CreateMap<DemandDoc, DemandDocDto>().ReverseMap();
            CreateMap<DemandDoc, CreateDemandDocDto>().ReverseMap();
            #endregion

            #region Acceptance Mappings   
            CreateMap<AcceptanceDto, Acceptance>().ReverseMap()
                .ForMember(d => d.ItemDetail, o => o.MapFrom(s => s.ItemDetail.PartNo))
                //.ForMember(d => d.DemandDate, o => o.MapFrom(s => s.ItemDetail.PartNo))
                .ForMember(d => d.ItemName, o => o.MapFrom(s => s.ItemDetail.NameOfItem))
                .ForMember(d => d.DemandDate, o => o.MapFrom(s => s.Demand.DemandDate))
                .ForMember(d => d.OuterLatterNo, o => o.MapFrom(s => s.Demand.LetterOuterNo))
                .ForMember(d => d.Demand, o => o.MapFrom(s => s.Demand.DemandNo))
                .ForMember(d => d.ItemInspection, o => o.MapFrom(s => s.ItemInspection.Name))
                .ForMember(d => d.Opinion, o => o.MapFrom(s => s.Opinion.Name))
                .ForMember(d => d.AcceptanceDocument, o => o.MapFrom<AcceptanceFileUrlResolver>());
            CreateMap<Acceptance, CreateAcceptanceDto>().ReverseMap();
            #endregion

            #region Manufacture Mappings   
            CreateMap<Manufacture, ManufactureDto>().ReverseMap();
            CreateMap<Manufacture, CreateManufactureDto>().ReverseMap();
            #endregion

            #region LocalAgent Mappings   
            CreateMap<LocalAgent, LocalAgentDto>().ReverseMap();
            CreateMap<LocalAgent, CreateLocalAgentDto>().ReverseMap();
            #endregion

            #region ItemCategoryType Mappings   
            CreateMap<ItemCategoryType, ItemCategoryTypeDto>().ReverseMap();
            CreateMap<ItemCategoryType, CreateItemCategoryTypeDto>().ReverseMap();
            #endregion


            #region DemandType Mappings   
            CreateMap<DemandType, DemandTypeDto>().ReverseMap();
            CreateMap<DemandType, CreateDemandTypeDto>().ReverseMap();
            #endregion


            #region Demand Mappings   
            CreateMap<DemandDto, Demand>().ReverseMap()
                .ForMember(d => d.DemandAuthority, o => o.MapFrom(s => s.DemandAuthority.Name))
                .ForMember(d => d.Deno, o => o.MapFrom(s => s.Deno.Name))
                .ForMember(d => d.Supplier, o => o.MapFrom(s => s.Supplier.CompanyName))
                .ForMember(d => d.Manufacture, o => o.MapFrom(s => s.Manufacture.Name))
                .ForMember(d => d.ConditionOfItem, o => o.MapFrom(s => s.ConditionOfItem.Name))
                .ForMember(d => d.OccasionOfDemand, o => o.MapFrom(s => s.OccasionOfDemand.Name))
                .ForMember(d => d.FiscalYear, o => o.MapFrom(s => s.FiscalYear.FiscalYearName))
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName))
                .ForMember(d => d.ItemDetail, o => o.MapFrom(s => s.ItemDetail.PartNo))
                .ForMember(d => d.PartNo, o => o.MapFrom(s => s.ItemDetail.PartNo))
                .ForMember(d => d.ItemName, o => o.MapFrom(s => s.ItemDetail.NameOfItem))
                .ForMember(d => d.ImcNumber, o => o.MapFrom(s => s.ItemDetail.ImcNumber))
                .ForMember(d => d.ItemCategory, o => o.MapFrom(s => s.ItemCategory.Name))
                .ForMember(d => d.DemandAuthority, o => o.MapFrom(s => s.Authority.Name))
                .ForMember(d => d.EquipmentName, o => o.MapFrom(s => s.EquipmentName.Name))
                .ForMember(d => d.ItemStatus, o => o.MapFrom(s => s.ItemStatus.Name))
                .ForMember(d => d.SpecDoc, o => o.MapFrom<FileUrlResolver>());

            CreateMap<Demand, CreateDemandDto>().ReverseMap();
            #endregion

            #region ItemDetail Mappings   

            CreateMap<ItemDetailDto, ItemDetail >().ReverseMap()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName))
                .ForMember(d => d.ItemCategory, o => o.MapFrom(s => s.ItemCategory.Name))
                .ForMember(d => d.EquipmentName, o => o.MapFrom(s => s.EquipmentName.Name))
                .ForMember(d => d.EquipmentPriority, o => o.MapFrom(s => s.EquipmentPriority.Name))
                 .ForMember(d => d.PhotoUp, o => o.MapFrom<PhotoUpUrlResolver>());
            CreateMap<ItemDetail, CreateItemDetailDto>().ReverseMap();
            #endregion

            #region ItemInspection Mappings   
            CreateMap<ItemInspection, ItemInspectionDto>().ReverseMap();
            CreateMap<ItemInspection, CreateItemInspectionDto>().ReverseMap();
            #endregion

            #region ItemStor Mappings   
            CreateMap<ItemStorDto, ItemStor>().ReverseMap()
             .ForMember(d => d.ItemDetail, o => o.MapFrom(s => s.ItemDetail.NameOfItem))
             .ForMember(d => d.Deno, o => o.MapFrom(s => s.Deno.Name))
             .ForMember(d => d.AcctStore, o => o.MapFrom(s => s.AcctStore.Name))
             .ForMember(d => d.PartNo, o => o.MapFrom(s => s.ItemDetail.PartNo))
             .ForMember(d => d.NameOfItem, o => o.MapFrom(s => s.ItemDetail.NameOfItem))
             .ForMember(d => d.Demand, o => o.MapFrom(s => s.Demand.EquipmentName.Name))
             .ForMember(d => d.EquipmentName, o => o.MapFrom(s => s.EquipmentName.Name))
             //.ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName))
             //.ForMember(d => d.ItemCategory, o => o.MapFrom(s => s.ItemCategory.Name))
             .ForMember(d => d.ToolsType, o => o.MapFrom(s => s.ToolsType.Name))
             .ForMember(d => d.ToolsLocation, o => o.MapFrom(s => s.ToolsLocation.ToolsLocationName))
             .ForMember(d => d.ToolsBoxName, o => o.MapFrom(s => s.ToolsBoxName.Name))
             .ForMember(d => d.OtherDoc, o => o.MapFrom<ItemStorFileUrlResolver>());
            CreateMap<ItemStor, CreateItemStorDto>().ReverseMap();
            #endregion

            //#region ItemStorDto Mappings   
            //CreateMap<ItemStor, ItemStorDto>().ReverseMap();
            //CreateMap<ItemStor, CreateItemStorDto>().ReverseMap();
            //#endregion

            #region IssueStatus Mappings   
            CreateMap<IssueStatus, IssueStatusDto>().ReverseMap();
            CreateMap<IssueStatus, CreateIssueStatusDto>().ReverseMap();
            #endregion

            #region IssueRegister Mappings   
            CreateMap<IssueRegisterDto, IssueRegister>().ReverseMap()
                .ForMember(d => d.Pno, o => o.MapFrom(s => s.TrainingCrew.Pno))
                .ForMember(d => d.Name, o => o.MapFrom(s => s.TrainingCrew.Name))
                .ForMember(d => d.ItemCategory, o => o.MapFrom(s => s.ItemCategory.Name))
                .ForMember(d => d.ItemName, o => o.MapFrom(s => s.ItemDetail.NameOfItem))
                .ForMember(d => d.PartNO, o => o.MapFrom(s => s.ItemDetail.PartNo))
                .ForMember(d => d.SurveyAuthority, o => o.MapFrom(s => s.SurveyAuthority.SurveyAuthorityName))
                .ForMember(d => d.SurveyItemType, o => o.MapFrom(s => s.SurveyItemType.SurveyItemTypeName))
                .ForMember(d => d.PartNO, o => o.MapFrom(s => s.ItemDetail.PartNo))
                .ForMember(d => d.Imc, o => o.MapFrom(s => s.ItemDetail.ImcNumber))
                .ForMember(d => d.ToolsLocation, o => o.MapFrom(s => s.ToolsLocation.ToolsLocationName));
            CreateMap<IssueRegister, CreateIssueRegisterDto>().ReverseMap();
            #endregion

            #region MaintenancePlanning Mappings   
            CreateMap<MaintenancePlanningDto, MaintenancePlanning>().ReverseMap()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName))
                .ForMember(d => d.AirCraftName, o => o.MapFrom(s => s.AirCraftName.Name))
                .ForMember(d => d.CategoryType, o => o.MapFrom(s => s.MaintenanceType.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.MaintenanceCategory.CategoryName))
                .ForMember(d => d.MPStatus, o => o.MapFrom(s => s.MaintenancePlanningStatus.Name))
                .ForMember(d => d.SubCategory, o => o.MapFrom(s => s.MaintenanceSubCategory.SubCategoryName));
            CreateMap<MaintenancePlanning, CreateMaintenancePlanningDto>().ReverseMap();
            #endregion

            #region MeaSquadronState Mappings   
            CreateMap<MeaSquadronStateDto, MeaSquadronState>().ReverseMap()
                .ForMember(d => d.PresentState, o => o.MapFrom(s => s.PresentState.Name));
            CreateMap<MeaSquadronState, CreateMeaSquadronStateDto>().ReverseMap();
            #endregion

            #region MaintenanceSchedule Mappings   
            CreateMap<MaintenanceScheduleDto, MaintenanceSchedule>().ReverseMap()
                .ForMember(d => d.MaintenancePlanning, o => o.MapFrom(s => s.MaintenancePlanning.NestInspDate))
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName))
                .ForMember(d => d.AirCraftName, o => o.MapFrom(s => s.AirCraftName.Name))
                .ForMember(d => d.CategoryType, o => o.MapFrom(s => s.MaintenanceType.Name))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.MaintenanceCategory.CategoryName))
                .ForMember(d => d.MPStatus, o => o.MapFrom(s => s.MaintenancePlanningStatus.Name))
                .ForMember(d => d.SubCategory, o => o.MapFrom(s => s.MaintenanceSubCategory.SubCategoryName));
            CreateMap<MaintenanceSchedule, CreateMaintenanceScheduleDto>().ReverseMap();
            #endregion

            #region TrainingCrew Mappings   
            CreateMap<TrainingCrewDto, TrainingCrew>().ReverseMap()
                .ForMember(d => d.Rank, o => o.MapFrom(s => s.Rank.Name))
                .ForMember(d => d.OfficersStatus, o => o.MapFrom(s => s.OfficersStatus.Name))
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName));
            CreateMap<TrainingCrew, CreateTrainingCrewDto>().ReverseMap();
            #endregion

            #region MaintenancePlanningStatus Mappings   
            CreateMap<MaintenancePlanningStatus, MaintenancePlanningStatusDto>().ReverseMap();
            CreateMap<MaintenancePlanningStatus, CreateMaintenancePlanningStatusDto>().ReverseMap();
            #endregion

            #region ToolsLocation Mappings   
            CreateMap<ToolsLocation, ToolsLocationDto>().ReverseMap();
            CreateMap<ToolsLocation, CreateToolsLocationDto>().ReverseMap();
            #endregion
             
            #region ToolsBoxName Mappings   
            CreateMap<ToolsBoxName, ToolsBoxNameDto>().ReverseMap();
            CreateMap<ToolsBoxName, CreateToolsBoxNameDto>().ReverseMap();
            #endregion

            #region SurveyItemType Mappings   
            CreateMap<SurveyItemType, SurveyItemTypeDto>().ReverseMap();
            CreateMap<SurveyItemType, CreateSurveyItemTypeDto>().ReverseMap();
            #endregion

            #region SurveyAuthority Mappings   
            CreateMap<SurveyAuthority, SurveyAuthorityDto>().ReverseMap();
            CreateMap<SurveyAuthority, CreateSurveyAuthorityDto>().ReverseMap();
            #endregion

            #region PmsNumberEntry Mappings   
            CreateMap<PmsNumberEntryDto, PmsNumberEntry>().ReverseMap()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName))
                .ForMember(d => d.ItemName, o => o.MapFrom(s => s.ItemDetail.NameOfItem));
            CreateMap<PmsNumberEntry, CreatePmsNumberEntryDto>().ReverseMap();
            #endregion

            #region JobDescriptionEntryPms Mappings   
            CreateMap<JobDescriptionEntryPmsDto, JobDescriptionEntryPms>().ReverseMap()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName))
                .ForMember(d => d.ItemName, o => o.MapFrom(s => s.ItemDetail.NameOfItem))
                .ForMember(d => d.MaintenanceType, o => o.MapFrom(s => s.MaintenanceType.Name))
                .ForMember(d => d.MaintenanceCategory, o => o.MapFrom(s => s.MaintenanceCategory.CategoryName))
                .ForMember(d => d.SpareRequiredStatusNavigation, o => o.MapFrom(s => s.SpareRequiredStatusNavigation.Name))
                .ForMember(d => d.ByWhom, o => o.MapFrom(s => s.ByWhom.Name));
            CreateMap<JobDescriptionEntryPms, CreateJobDescriptionEntryPmsDto>().ReverseMap();
            #endregion

            #region JobCategoryMaintenence Mappings 
            CreateMap<JobCategoryMaintenence, JobCategoryMaintenenceDto>().ReverseMap();
            CreateMap<JobCategoryMaintenence, CreateJobCategoryMaintenenceDto>().ReverseMap();
            #endregion

            #region SpareRequiredEntry Mappings   
            CreateMap<SpareRequiredEntryDto,SpareRequiredEntry>().ReverseMap()
                .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.DepartmentName.SchoolName))
                .ForMember(d => d.JobDescription, o => o.MapFrom(s => s.JobDescriptionEntryPms.JobDescription))
                .ForMember(d => d.ItemName, o => o.MapFrom(s => s.ItemDetail.NameOfItem));
            CreateMap<SpareRequiredEntry, CreateSpareRequiredEntryDto>().ReverseMap();
            #endregion

            #region PreventiveMaintenance Mappings   
            CreateMap<PreventiveMaintenanceDto, PreventiveMaintenance>().ReverseMap()
                .ForMember(d => d.PattNo, o => o.MapFrom(s => s.ItemDetail.PartNo))
                .ForMember(d => d.ItemName, o => o.MapFrom(s => s.ItemDetail.NameOfItem))
                .ForMember(d => d.Category, o => o.MapFrom(s => s.MaintenanceCategory.CategoryName))
                .ForMember(d => d.MaintenanceType, o => o.MapFrom(s => s.MaintenanceType.Name))
                .ForMember(d => d.Jic, o => o.MapFrom(s => s.JobDescriptionEntryPms.Jic))
                .ForMember(d => d.JobDescription, o => o.MapFrom(s => s.JobDescriptionEntryPms.JobDescription))
                .ForMember(d => d.ByWhome, o => o.MapFrom(s => s.JobDescriptionEntryPms.ByWhom.Name))
                .ForMember(d => d.MaintenenceCategory, o => o.MapFrom(s => s.MaintenanceCategory.CategoryName))
                .ForMember(d => d.SubCategory, o => o.MapFrom(s => s.MaintenanceSubCategory.SubCategoryName));
            CreateMap<PreventiveMaintenance, CreatePreventiveMaintenanceDto>().ReverseMap();
            #endregion

            #region ScheduleMaintenence Mapping    
            CreateMap<ScheduleMaintenanceDto, ScheduleMaintenance>().ReverseMap()
                  .ForMember(d => d.JobDescriptionScheduleMaintenence, o => o.MapFrom(s => s.JobDescriptionScheduleMaintenence.JobDescription))
                  .ForMember(d => d.FiscalYear, o => o.MapFrom(s => s.FiscalYear.FiscalYearName))
                  .ForMember(d => d.ByWhom, o => o.MapFrom(s => s.ByWhom.Name))
                  .ForMember(d => d.Progress, o => o.MapFrom(s => s.Progres.Name));
            CreateMap<ScheduleMaintenance, CreateScheduleMaintenanceDto>().ReverseMap();
            #endregion

            #region UserManual Mappings
            CreateMap<UserManualDto, UserManual>().ReverseMap()
                .ForMember(d => d.Doc, o => o.MapFrom<UserManualUrlResolver>());
            CreateMap<UserManual, CreateUserManualDto>().ReverseMap();
            #endregion
        }
    } 
}


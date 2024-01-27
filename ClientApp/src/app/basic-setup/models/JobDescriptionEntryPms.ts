export interface JobDescriptionEntryPms {
    jobDescriptionEntryPmsId: number,
    unitId:number;
    departmentNameId:number;
    equipmentNameId:number;
    maintenanceTypeId:number;
    maintenanceCategoryId:number;
    byWhomId:number;
    pmsNumberEntryId:number;
    itemDetailId:any;
    jobDescription:string,
    jic:string;
    spareRequiredStatus:boolean;
    remarks:string,
    spareRequiredStatusId:number;
 isActive: boolean
   
}
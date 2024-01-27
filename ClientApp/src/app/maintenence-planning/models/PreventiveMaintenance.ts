export interface PreventiveMaintenance {
    preventiveMaintenanceId: number;
    unitId: number;
    departmentNameId: number;
    issueRegisterId:number;
    itemDetailId:number;
    partNo:string,
    pattNo:any,
    maintenanceSubCategoryId:number;
    maintenanceCategoryId:number;
    pmsno:string;
    completedDate:Date;
    completedStatus:number;
    remarks:string;
    isActive: boolean;
    maintenanceTypeId:number;
    maintenenceCategory:string;
    jobDescriptionEntryPmsId:number;
}
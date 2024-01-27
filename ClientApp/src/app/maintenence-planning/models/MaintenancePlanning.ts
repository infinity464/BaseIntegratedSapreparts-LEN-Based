export interface MaintenancePlanning {
    maintenancePlanningId: number;
    airCraftNameId: number;
    slNo: string;
    maintenanceTypeId: number;
    maintenanceCategoryId: number;
    maintenanceSubCategoryId: number;
    maintenancePlanningStatusId: number;
    departmentNameId: number;
    lastInspDate: Date;
    nestInspDate:Date;
    lastInspectionDay:string;
    nextInspectionDay:string;
    lastInspectionFH:string;
    nextInspectionFH:string;
    lastInspectionOH:string;
    nextInspectionOH:string;
    extensionGiven: string;
    extensionDay:string;
    requiredDay: string;
    maintenanceDocument:string;
    extensionDocument: string;
    othersDocument:string;
    jobListDocument: string;
    requiredSpearsDoc:string;
    requiredToolsDoc: string;
    requiredConsumablesDoc:string;
    toleranceDocument: string;
    remarks:string;
    isActive: boolean;
   
}
export interface ScheduleMaintenence {
  scheduleMaintenanceId: number,
  unitId: number,
  departmentNameId: number,
  issueRegisterId: number,
  itemDetailId: number,
  scheduleMaintenenceTypeId: number,
  fiscalYearId: number,
  jobDescriptionScheduleMaintenenceId: number,
  byWhomId: number,
  drNo: string,
  startDate: Date,
  completedDate:Date,
  menuPosition: number,
  isActive: boolean,
  progresId:boolean,
  completedStatus:boolean,
  jobDescriptionScheduleMaintenence:string
  progress:string
}
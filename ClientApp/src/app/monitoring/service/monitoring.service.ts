import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { Demand } from 'src/app/spares-management/models/Demand';
@Injectable({
  providedIn: 'root'
})
export class MonitoringService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getRemainProcurementQty(id){
    return this.http.get<any>(this.baseUrl + '/dashboard/get-remainProcurementQty?departmentId='+id)
  }

  // dashboard security dropdown API

  getSelectedBaseSchool(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchools')
  }

  getItemNameByIdRequest(unitId, departmentNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/preventive-maintenance/get-itemNameByUnitAndDepartmentIdRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'')
  }
  getItemNameByItemDetailByPreventive(unitId, departmentNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-selecteditemNameByParametersForPreventive?unitId='+unitId+'&departmentId='+departmentNameId+'')
  }
  getPmsNoByItemDetailId(itemDetailId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/preventive-maintenance/get-selectedPmsNoByItemDetailId?itemDetailId='+itemDetailId)
  }
  getCategoryByIdRequest(unitId, departmentNameId,itemDetailId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/preventive-maintenance/get-categoryByUnitAndDepartmentAndItemDetailIdRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemDetailId='+itemDetailId+'')
  }
  getSubCategoryByIdRequest(unitId, departmentNameId,itemDetailId,maintenanceTypeId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/preventive-maintenance/get-subCategoryByUnitAndDepartmentAndItemDetailAndCategoryIdRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemDetailId='+itemDetailId+'&maintenanceTypeId='+maintenanceTypeId+'')
  }
  getSelectedIssueType() {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/issue-type/get-selectedIssueTypes');
  }
  getListForEquipementNonOparatonal(unitId,departmentId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/issue-register/get-issueRegisterListForEquipmentNonOparatonalByParameterRequest?unitId='+unitId+'&departmentNameId='+departmentId)
  }
  getSelectedItemCategory(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-category/get-selectedItemCategory')
  }
  updateStatus(issueRegisterId,status){
    return this.http.get<any>(this.baseUrl + '/issue-register/update-equipNonOPLStatusOnIssueRegister?issueRegisterId='+issueRegisterId+'&status='+status)
  }

  getSelectedSchoolName(baseNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  }
  
  findItemDetails(unitId,departmentId,itemCategoryId,equipmentNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/find-itemDetailsByParameters?unitId='+unitId+'&departmentId='+departmentId+'&itemCategoryId='+itemCategoryId+'&equipmentNameId='+equipmentNameId)
  }

  
  findAvailableQty(unitId,departmentId,itemCategoryId,equipmentNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/find-availableQtyByParameters?unitId='+unitId+'&departmentId='+departmentId+'&itemCategoryId='+itemCategoryId+'&equipmentNameId='+equipmentNameId)
  }

  
  findDemands(unitId,departmentId,itemCategoryId,equipmentNameId,itemDetailId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/find-demandsByParameters?unitId='+unitId+'&departmentId='+departmentId+'&itemCategoryId='+itemCategoryId+'&equipmentNameId='+equipmentNameId+'&itemDetailId='+itemDetailId)
  }

  
  findCorrective(unitId,departmentId,itemCategoryId,equipmentNameId,itemDetailId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/corrective-maintenence/find-correctiveByParameters?unitId='+unitId+'&departmentId='+departmentId+'&itemCategoryId='+itemCategoryId+'&equipmentNameId='+equipmentNameId+'&itemDetailId='+itemDetailId)
  }

  findOngoingCorrective(unitId,departmentId,itemCategoryId,equipmentNameId,itemDetailId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/corrective-maintenence/find-ongoingCorrectiveByParameters?unitId='+unitId+'&departmentId='+departmentId+'&itemCategoryId='+itemCategoryId+'&equipmentNameId='+equipmentNameId+'&itemDetailId='+itemDetailId+'')
  }
  
  findProcurements(unitId,departmentId,itemCategoryId,equipmentNameId,demandId){
    return this.http.get<any[]>(this.baseUrl + '/procurement/find-procurementsByParameters?unitId='+unitId+'&departmentId='+departmentId+'&itemCategoryId='+itemCategoryId+'&equipmentNameId='+equipmentNameId+'&demandId='+demandId)
  }
  findScheduledMaintanenceJobCount(unitId,departmentId,issueRegisterId,scheduleMaintenenceTypeId,fiscalYearId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/schedule-maintenance/find-scheduleMaintenancesCountByParameters?unitId='+unitId+'&departmentId='+departmentId+'&issueRegisterId='+issueRegisterId+'&scheduleMaintenenceTypeId='+scheduleMaintenenceTypeId+'&fiscalYearId='+fiscalYearId+'')
  }
  findScheduledMaintanenceJobCountTotal(unitId,departmentId,issueRegisterId,scheduleMaintenenceTypeId,fiscalYearId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/schedule-maintenance/find-scheduleTotalParameters?unitId='+unitId+'&departmentId='+departmentId+'&issueRegisterId='+issueRegisterId+'&scheduleMaintenenceTypeId='+scheduleMaintenenceTypeId+'&fiscalYearId='+fiscalYearId+'')
  }

  // schedule-maintenance/find-scheduleTotalParameters?unitId=7&departmentId=2&issueRegisterId=4&scheduleMaintenenceTypeId=441&fiscalYearId=2
  findScheduledMaintanences(unitId,departmentId,issueRegisterId,scheduleMaintenenceTypeId,fiscalYearId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/schedule-maintenance/find-scheduleMaintenancesByParameters?unitId='+unitId+'&departmentId='+departmentId+'&issueRegisterId='+issueRegisterId+'&scheduleMaintenenceTypeId='+scheduleMaintenenceTypeId+'&fiscalYearId='+fiscalYearId)
  }

  
  findSft(unitId,departmentId,demandId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/acceptance/find-sftByParameters?unitId='+unitId+'&departmentId='+departmentId+'&demandId='+demandId)
  }

  

  
  findIssueRegister(unitId,departmentId,issueTypeId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/issue-register/find-issueRegisterByParameters?unitId='+unitId+'&departmentId='+departmentId+'&issueTypeId='+issueTypeId)
  }

  
  GetSelectedItemNames(unitId,departmentId,itemCategoryId,equipmentNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-selecteditemNameByParameters?unitId='+unitId+'&departmentId='+departmentId+'&itemCategoryId='+itemCategoryId+'&equipmentNameId='+equipmentNameId)
  }
  GetSelectedNameofItem(unitId,departmentId,itemCategoryId,equipmentNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-nameofItemByUnitAndDepartmentAndCategoryIdRequest?unitId='+unitId+'&departmentNameId='+departmentId+'&itemCategoryId='+itemCategoryId+'&equipmentNameId='+equipmentNameId)
  }
  
  finddemandStates(unitId,departmentId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/find-demandStates?unitId='+unitId+'&departmentId='+departmentId)
  }
  getSurveyMonitoringList(unitId,departmentId,itemCategoryId,equipmentNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/issue-register/find-issueRegisterListForSurveyMonitoringByParameters?unitId='+unitId+'&departmentId='+departmentId+'&itemCategoryId='+itemCategoryId+'&equipmentNameId='+equipmentNameId)
  }

  getSelectedDemandNoByParams(unitId,departmentId,itemCategoryId,equipmentNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-selectedDemandNoByUnitAndDepartmentAndCategoryIdRequest?unitId='+unitId+'&departmentNameId='+departmentId+'&itemCategoryId='+itemCategoryId+'&equipmentNameId='+equipmentNameId)
  }
  // findPreventiveMaintenance(unitId,departmentId,itemDetailId){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/preventive-maintenance/find-preventiveListForMonitorintByParameters?unitId='+unitId+'&departmentId='+departmentId+'&itemDetailId='+itemDetailId)
  // }

  findPreventiveMaintenance(unitId,departmentId,itemDetailId,maintenenceCategoryId,maintenanceTypeId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/preventive-maintenance/find-preventiveListForMonitorintByParameters?unitId='+unitId+'&departmentId='+departmentId+'&itemDetailId='+itemDetailId+'&maintenenceCategoryId='+maintenenceCategoryId+'&maintenenceTypeId='+maintenanceTypeId+'')
  }

  getSelectedDemandNo(unitId,departmentId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-selectedDemandNo?unitId='+unitId+'&departmentId='+departmentId)
  }

  getSelectedEquipmentName(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equipment-name/get-selectedEquipmentName')
  }
  
  find(id: number) {
    return this.http.get<Demand>(this.baseUrl + '/demand/get-demandDetail/' + id);
  }
  

  // dashboard security dropdown API


  getPendingDemands(id){
    return this.http.get<any>(this.baseUrl + '/dashboard/get-pendingDemands?departmentId='+id)
  }
  getTrainingCrew(id){
    return this.http.get<any>(this.baseUrl + '/dashboard/get-trainingCrew?departmentId='+id)
  }
  getPendingProcurements(id){
    return this.http.get<any>(this.baseUrl + '/dashboard/get-pendingProcurements?departmentId='+id)
  }

  getPendingAcceptances(id){
    return this.http.get<any>(this.baseUrl + '/dashboard/get-pendingAcceptances?departmentId='+id)
  }

  getAvailableQty(id){
    return this.http.get<any>(this.baseUrl + '/dashboard/get-availableQty?departmentId='+id)
  }
  getFlyingTimeByAricraft(id){
    return this.http.get<any>(this.baseUrl + '/dashboard/get-flyingTimeByAricraft?departmentId='+id)
  }
  getAricraftFlying(current,id){
    return this.http.get<any>(this.baseUrl + '/dashboard/get-aricraftFlying?currentDate='+current+'&departmentId='+id)
  }
  getDemandSpGetCompleteStatus(id){
    return this.http.get<any>(this.baseUrl + '/dashboard/get-SpGetCompleteStatus?departmentId='+id)
  }

}

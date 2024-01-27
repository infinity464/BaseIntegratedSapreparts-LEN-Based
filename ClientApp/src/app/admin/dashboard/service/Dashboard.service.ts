import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }

  getRemainProcurementQty(id){
    return this.http.get<any>(this.baseUrl + '/dashboard/get-remainProcurementQty?departmentId='+id)
  }

  // dashboard security dropdown API

  getSelectedBaseSchool(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchools')
  }

  getSelectedSchoolName(baseNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  }
  
  findItemDetails(unitId,departmentId,itemCategoryId,equipmentNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/find-itemDetailsByParameters?unitId='+unitId+'&departmentId='+departmentId+'&itemCategoryId='+itemCategoryId+'&equipmentNameId='+equipmentNameId)
  }

  getEquipmentStateCount(unitId,departmentId,operationStatus){
    return this.http.get<any[]>(this.baseUrl + '/dashboard/get-equipmentStateListForDb?unitId='+unitId+'&departmentId='+departmentId+'&operationStatus='+operationStatus)
  }

  getSpareToolAndEquipmentCount(unitId,departmentId,itemCategoryId){
    return this.http.get<any[]>(this.baseUrl + '/dashboard/get-spareEquipmentandToolsCount?unitId='+unitId+'&departmentId='+departmentId+'&itemCategoryId='+itemCategoryId+'')
  }
  getSpareCount(unitId,departmentId,itemCategoryId){
    return this.http.get<any[]>(this.baseUrl + '/dashboard/get-spareCount?unitId='+unitId+'&departmentNameId='+departmentId+'&itemCategoryId='+itemCategoryId+'')
  }
  getToolsCount(unitId,departmentId,itemCategoryId){
    return this.http.get<any[]>(this.baseUrl + '/dashboard/get-toolsCount?unitId='+unitId+'&departmentNameId='+departmentId+'&itemCategoryId='+itemCategoryId+'')
  }
  // dashboard security dropdown API

  getPendingDemands(id){
    return this.http.get<any>(this.baseUrl + '/dashboard/get-pendingDemands?departmentId='+id)
  }
  getSelectedFiscalYear(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/fiscal-year/get-selectedFiscalYear')
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

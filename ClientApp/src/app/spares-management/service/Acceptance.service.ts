import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IAcceptancePagination, AcceptancePagination } from '../models/AcceptancePagination'
import { Acceptance } from '../models/Acceptance';
import { map } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class AcceptanceService {
  baseUrl = environment.apiUrl;
  Acceptances: Acceptance[] = [];
  AcceptancePagination = new AcceptancePagination();
  constructor(private http: HttpClient) { }

  getAcceptances(pageNumber, pageSize, searchText,sparesCategoryId) {
    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('sparesCategoryId', sparesCategoryId.toString());
    
    return this.http.get<IAcceptancePagination>(this.baseUrl + '/acceptance/get-acceptances', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Acceptances = [...this.Acceptances, ...response.body.items];
        this.AcceptancePagination = response.body;
        return this.AcceptancePagination;
      })
    );
   
  }
  
  getAcceptanceList(pageNumber, pageSize, searchText,departmentNameId,itemCategoryId,unitId) {
    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('departmentNameId', departmentNameId.toString());
    params = params.append('itemCategoryId', itemCategoryId.toString());
    params = params.append('unitId', unitId.toString());
    
    return this.http.get<IAcceptancePagination>(this.baseUrl + '/acceptance/get-acceptanceListByParameterRequest', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Acceptances = [...this.Acceptances, ...response.body.items];
        this.AcceptancePagination = response.body;
        return this.AcceptancePagination;
      })
    );
   
  }
  getSelectedProcurementQty(unitId,departmentNameId,itemCategoryId,demandId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/procurement/get-selectedProcurementQtyFromProcurementByParameterRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemCategoryId='+itemCategoryId+'&demandId='+demandId+'')
  }

  //procurement/get-selectedProcurementQtyFromProcurementByParameterRequest?unitId=14&departmentNameId=29&itemCategoryId=1&demandId=2292
  getSelectedDemandQty(unitId,departmentNameId,itemCategoryId,demandId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-selectedDemandQtyFromDemandByParameterRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemCategoryId='+itemCategoryId+'&demandId='+demandId+'')
  }
  getSelectedDemandDate(unitId,departmentNameId,itemCategoryId,demandId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-selectedDateFromDemandByParameterRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemCategoryId='+itemCategoryId+'&demandId='+demandId+'')
  }
  getSelectedDemandNo(unitId,departmentNameId,itemCategoryId,storeStatus,sftStatus){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-selectedDemandNoByDepartmentIdUnitIdAndItemCategoryIdStatusRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemCategoryId='+itemCategoryId+'&storeStatus='+storeStatus+'&sftStatus='+sftStatus+'')
  }
  getSelectedOpinion(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/opinion/get-selectedOpinions');
  }

  getSelectedItemInspection(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-inspection/get-selectedItemInspections');
  }
  getPartNoPassItemCategoryIdInAcceptance(itemDetailId:number){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/acceptance/get-PartNoPassItemCategoryIdInAcceptance?itemDetailId=' + itemDetailId);
  }
  getselectedItemDetails(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-selectedItemDetails')
  }

  getselectedProcurements(procurementId, spearCategoryId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/procurement/get-partnoFromProcurementByDepartmentNameId?departmentNameId='+procurementId+'&sparesCategoryId='+spearCategoryId)
  }

  getselectedProcurementsOnUpdate(procurementId, spearCategoryId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/procurement/get-partnoFromProcurementForUpdateByDepartmentNameId?departmentNameId='+procurementId+'&sparesCategoryId='+spearCategoryId)
  }
  getSelectedDemandNoByStatus(unitId,departmentNameId,itemCategoryId,itemStatusId,status){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-selectedDemandNoByItemStatus?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemCategoryId='+itemCategoryId+'&itemStatusId='+itemStatusId+'&status='+status)
  }
  getselectedSourceOfSupplys(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/source-of-supply/get-selectedSourceOfSupplys')
  }
  getSelectedSchoolName(baseNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  }

  getSelectedSchoolByFourthLevel(fourthLevel){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNamesByFourthLevel?fourthLevel='+fourthLevel+'')
  }
  getselectedManufactures(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/manufacture/get-selectedManufactures')
  }
  getselectedPrincipalNames(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/principal-name/get-selectedPrincipalNames')
  }
  getselectedPlaceOfDeliverys(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/place-of-delivery/get-selectedPlaceOfDeliverys')
  }
  // getselectedDemandAuthority(){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/demand-authority/get-selectedDemandAuthority')
  // }
  getselectedConditionOfItem(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/condition-of-item/get-selectedConditionOfItem')
  }
  getselectedProcurementStatuses(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/procurement-status/get-selectedProcurementStatuses')
  }
  getselectedDepartmentNames(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/department-name/get-selectedDepartmentNames')
  }
  getselectedItemInspections(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-inspection/get-selectedItemInspections')
  }
  getAcceptanceListByDepartmentNameId(pageNumber, pageSize, searchText,sparesCategoryId,departmentId) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('sparesCategoryId', sparesCategoryId.toString());
    params = params.append('departmentNameId', departmentId.toString());
    
    return this.http.get<IAcceptancePagination>(this.baseUrl + '/acceptance/get-AcceptanceListByDepartmentNameId', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Acceptances = [...this.Acceptances, ...response.body.items];
        this.AcceptancePagination = response.body;
        return this.AcceptancePagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<Acceptance>(this.baseUrl + '/acceptance/get-acceptanceDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/acceptance/update-acceptance/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/acceptance/save-acceptance', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/acceptance/delete-acceptance/'+id);
  }

}

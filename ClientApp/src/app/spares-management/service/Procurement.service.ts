import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IProcurementPagination, ProcurementPagination } from '../models/ProcurementPagination'
import { Procurement } from '../models/Procurement';
import { map } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ProcurementService {
  baseUrl = environment.apiUrl;
  Procurements: Procurement[] = [];
  ProcurementPagination = new ProcurementPagination();
  constructor(private http: HttpClient) { }

  getProcurements(pageNumber, pageSize, searchText,sparesCategoryId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('sparesCategoryId', sparesCategoryId.toString());
    
    return this.http.get<IProcurementPagination>(this.baseUrl + '/procurement/get-procurements', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Procurements = [...this.Procurements, ...response.body.items];
        this.ProcurementPagination = response.body;
        return this.ProcurementPagination;
      })
    );
  }
  getSelectedDemandNo(unitId,departmentNameId,itemCategoryId,storeStatus,procurementStatus){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-selectedDemandNoByDepartmentIdUnitIdAndItemCategoryIdRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemCategoryId='+itemCategoryId+'&storeStatus='+storeStatus+'&procurementStatus='+procurementStatus+'')
  }
  getSelectedDemandNoByStatus(unitId,departmentNameId,itemCategoryId,itemStatusId,status){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-selectedDemandNoByItemStatus?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemCategoryId='+itemCategoryId+'&itemStatusId='+itemStatusId+'&status='+status)
  }
  

  getSelectedDemandById(demandId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-selectedDemandNoByDeamndId?demandId='+demandId+'')
  }

  getSelectedProcurementType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/procurement-type/get-selectedProcurementTypes')
  }
  
  getSelectedRefNo(demandId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-selectedRefNoByDemandId?demandId='+demandId+'')
  }
  getSelectedProcurementStatus(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/procurement-status/get-selectedProcurementStatuses')
  }
  getSelectedItemCategory(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-category/get-selectedItemCategory')
  }
  getProcurementSpGetPrCompleteStatus(){
    return this.http.get<any>(this.baseUrl + '/procurement/get-SpGetPrCompleteStatus')
  }
  getPartNoPassItemCategoryIdInProcurement(itemDetailId:number){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/procurement/get-PartNoPassItemCategoryIdInProcurement?itemDetailId=' + itemDetailId);
  }
  getPartNoForProcurement(itemStatusId,procurementStatus){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-partnoFromDemandByItemStatusIdAndProcurementStatus?itemStatusId='+itemStatusId+'&procurementStatus='+procurementStatus+'')
  }
  getselectedItemDetails(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-selectedItemDetails')
  }
  getselectedPrincipalNames(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/principal-name/get-selectedPrincipalNames')
  }
  getselectedManufacture(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/manufacture/get-selectedManufactures')
  }
  getselectedProcurementStatus(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/procurement-status/get-selectedProcurementStatuss')
  }
  // getselectedLocalAgents(){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/local-agent/get-selectedLocalAgents')
  // }
  getselectedSupplier(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/suppliers/get-selectedSupplier')
  }
  getselectedSupplierA(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/suppliers/get-selectedSupplier')
  }
  getselectedSupplierM(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/suppliers/get-selectedSupplier')
  }
  getselectedPartOfShipments(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/part-of-shipment/get-selectedPartOfShipments')
  }

  GetselectedProcurementById(procurementId){
    return this.http.get<Procurement[]>(this.baseUrl + '/procurement/get-selectedProcurementById?procurementId='+procurementId);
  }

  getselectedDemands(departmentId,spearCategoryId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-partnoFromDemandByDepartmentNameId?departmentNameId='+departmentId+'&sparesCategoryId='+spearCategoryId)
  }

  getselectedDemandsOnUpdate(departmentId,spearCategoryId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-partnoFromDemandForUpdateByDepartmentNameId?departmentNameId='+departmentId+'&sparesCategoryId='+spearCategoryId)
  }

  getselectedCstTecs(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/cst-tec/get-selectedCstTec')
  }
  getProcurementListByDepartmentNameId(pageNumber, pageSize, searchText,unitId,departmentNameId,itemCategoryId) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('unitId', unitId.toString());
    params = params.append('departmentNameId', departmentNameId.toString());
    params = params.append('itemCategoryId', itemCategoryId.toString());
    return this.http.get<IProcurementPagination>(this.baseUrl + '/procurement/get-ProcurementListByDepartmentNameId', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Procurements = [...this.Procurements, ...response.body.items];
        this.ProcurementPagination = response.body;
        return this.ProcurementPagination;
      })
    );
  }

  find(id: number) {
    return this.http.get<Procurement>(this.baseUrl + '/procurement/get-procurementDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/procurement/update-procurement/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/procurement/save-procurement', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/procurement/delete-procurement/'+id);
  }

}

import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IDemandPagination,DemandPagination } from '../models/IDemandPagination'
import { Demand } from '../models/Demand';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class DemandService {
  baseUrl = environment.apiUrl;
  Demands: Demand[] = [];
  DemandPagination = new DemandPagination();
  constructor(private http: HttpClient) { }

  getDemands(pageNumber, pageSize, searchText,sparesCategoryId) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('sparesCategoryId', sparesCategoryId.toString());

    
    return this.http.get<IDemandPagination>(this.baseUrl + '/demand/get-demands', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Demands = [...this.Demands, ...response.body.items];
        this.DemandPagination = response.body;
        return this.DemandPagination;
      })
    );
   // 
  }
  getDemandSpGetCompleteStatus(id){
    return this.http.get<any>(this.baseUrl + '/demand/get-SpGetCompleteStatus?departmentId='+id)
  }
  

  //autocomplete for Course item-detail/get-autocompletePartNoByNameForSpares?partNo=Spar123'
  getSelectedCourseByName(courseName){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-autocompletePartNoByName?partNo='+courseName)
      .pipe(
        map((response:[]) => response.map(item => item))
      )
  }

 //autocomplete for By PartNo   
  // getSelectedPartNoByNameForSpares(partNo){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-autocompletePartNoByNameForSpares?partNo='+partNo+'')
  //     .pipe(
  //       map((response:[]) => response.map(item => item))
  //     )
  // }

  getSelectedPartNoByNameForSpares(partNo,unitId,departmentNameId,itemCategoryId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-autocompletePartNoByNameForSpares?partNo='+partNo+'&unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemCategoryId='+itemCategoryId+'')
      .pipe(
        map((response:[]) => response.map(item => item))
      )
  }

  getSelectedImcForSpares(imc,unitId,departmentNameId,itemCategoryId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-autocompleteImcByNameForSpares?imc='+imc+'&unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemCategoryId='+itemCategoryId+'')
      .pipe(
        map((response:[]) => response.map(item => item))
      )
  }
  getSelectedAuthoritys(unit,departmentId,itemCategoryId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/survey/get-selectedSurveyByParameterRequest?unitId='+unit+'&departmentNameId='+departmentId+'&itemCategoryId='+itemCategoryId+'')
  }
  getSelectedAuthority(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/authority/get-selectedAuthoritys')
  }
  getSelectedSurveyNo(unitId,departmentNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/issue-register/get-selectedSurveyNoForDemandParameterRequest?unitId='+unitId+'&departmentNameId='+departmentNameId)
  }
  getSelectedItemDetails(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-selectedItemDetails')
  }
  getSelectedTypeOfDemand(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand-type/get-selectedDemandTypes')
  }
  getSelectedDeno(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/deno/get-selectedDeno')
  }
  getSelectedTrade(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/trade/get-selectedTrades')
  }
  
  getSelectedManufacture(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/manufacture/get-selectedManufactures')
  }
  getSelectedSuplier(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/suppliers/get-selectedSupplier')
  }
  getDemandListByDepartmentNameId(pageNumber, pageSize, searchText,unitId,departmentNameId,itemCategoryId) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('unitId', unitId.toString());
    params = params.append('departmentNameId', departmentNameId.toString());
    params = params.append('itemCategoryId', itemCategoryId.toString());
    return this.http.get<IDemandPagination>(this.baseUrl + '/demand/get-DemandListForSparesByDepartmentNameId', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Demands = [...this.Demands, ...response.body.items];
        this.DemandPagination = response.body;
        return this.DemandPagination;
      })
    );
   // 
  }
  // getaDemandListByDepartmentNameId( departmentNameId:number){
  //   return this.http.get<Demand[]>(this.baseUrl + '/demand/get-DemandListByDepartmentNameId?departmentNameId='+departmentNameId);
  //  }
  getPartNoPassItemCategoryIdInDemand(itemDetailId:number){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-PartNoPassItemCategoryIdInDemand?itemDetailId=' + itemDetailId);
  }
getSelectedEquipmentName(sparesCategoryId,departmentId){
  return this.http.get<SelectedModel[]>(this.baseUrl + '/equipment-name/get-selectedEquipmentNameBySparesCategoryIdAndDepartmentNameId?sparesCategoryId='+sparesCategoryId+'&departmentNameId='+departmentId+'');
}

getSelectedItemStatus(){
  return this.http.get<SelectedModel[]>(this.baseUrl + '/item-status/get-selectedItemStatus');
}
getSelectedGroupName(){
  return this.http.get<SelectedModel[]>(this.baseUrl + '/group/get-selectedGroups');
}
  getSelectedFiscalYear(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/fiscal-year/get-selectedFiscalYear')
  }
  getSelectedItemType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-type/get-selectedItemType')
  }
  getSelectedOccasionOfDemand(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/occasion-of-demand/get-selectedOccasionOfDemands')
  }
  getSelectedDemandAuthority(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand-authority/get-selectedDemandAuthority')
  }

  getSelectedDepartmentName(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/department-name/get-selectedDepartmentNames')
  }
  getSelectedSchoolName(baseNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  }
  getSelectedItemCategory(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-category/get-selectedItemCategory')
  }
  getSelectedConditionOfItem(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/condition-of-item/get-selectedConditionOfItem')
  }

  GetselectedDemandById(demandId){
    return this.http.get<Demand[]>(this.baseUrl + '/demand/get-selectedDemandById?demandId='+demandId);
  }
  GetselectedDemandByParameterSpRequest(demandId){
    return this.http.get<Demand[]>(this.baseUrl + '/demand/get-demandProcurementAndSftbyParameterSpRequest?demandId='+demandId+'');
  }
  
  find(id: number) {
    return this.http.get<Demand>(this.baseUrl + '/demand/get-demandDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/demand/update-demand/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/demand/save-demand', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/demand/delete-demand/'+id);
  }

}

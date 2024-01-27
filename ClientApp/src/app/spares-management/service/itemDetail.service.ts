import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IItemDetailPagination, ItemDetailPagination } from '../models/itemDetailPagination'
import { ItemDetail } from '../models/itemDetail';
import { map } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { EquipmentName } from 'src/app/basic-setup/models/EquipmentName';
@Injectable({
  providedIn: 'root'
})
export class ItemDetailService {
  baseUrl = environment.apiUrl;
  ItemDetails: ItemDetail[] = [];
  ItemDetailPagination = new ItemDetailPagination();
  constructor(private http: HttpClient) { }

  getItemDetails(pageNumber, pageSize, searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    
    return this.http.get<IItemDetailPagination>(this.baseUrl + '/item-detail/get-ItemDetails', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ItemDetails = [...this.ItemDetails, ...response.body.items];
        this.ItemDetailPagination = response.body;
        return this.ItemDetailPagination;
      })
    );
   
  }

  getItemDetailsForTools(pageNumber, pageSize, searchText,sparesCategoryId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('sparesCategoryId', sparesCategoryId.toString());
    //item-detail/get-itemDetailsForTools?PageSize=5&PageNumber=1&sparesCategoryId=2
    return this.http.get<IItemDetailPagination>(this.baseUrl + '/item-detail/get-itemDetailsForTools', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ItemDetails = [...this.ItemDetails, ...response.body.items];
        this.ItemDetailPagination = response.body;
        return this.ItemDetailPagination;
      })
    );
   
  }

  getSelectedEquipmentNameForAutocomplete(name){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equipment-name/get-autocompleteEquipmentName?name='+name+'')
      .pipe(
        map((response:[]) => response.map(item => item))
      )
  }
  
  getSelectedEquipmentPriority(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equipment-priority/get-selectedEquipmentPriority')
  }

  getEquipmentNameBySparesCategoryIdAndDepartmentNameId( sparesCategoryId:number,departmentNameId:number ){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equipment-name/get-selectedEquipmentNameBySparesCategoryIdAndDepartmentNameId?sparesCategoryId='+sparesCategoryId+'&departmentNameId='+departmentNameId+'');
  }
  getSelectedItemCategory(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-category/get-selectedItemCategory')
  }
  // getselectedEquipmentName(){equipment-name/get-selectedEquipmentNameBySparesCategoryIdAndDepartmentNameId?sparesCategoryId=1&departmentNameId=20
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/equipment-name/get-selectedEquipmentName')
  // }
  getselectedSparesCategory(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/spares-category/get-selectedSparesCategory')
  }
  getEquipmentNameBySparesCategoryId( sparesCategoryId:number ){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equipment-name/get-selectedEquipmentNameBySparesCategoryId?sparesCategoryId='+sparesCategoryId);
  }
  
  getselectedItemDetail(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-selectedItemDetails')
  }

  getselectedEquipmentName(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/equipment-name/get-selectedEquipmentName')
  }

  getselectedItemDetailByDepartmentNameId(departmentNameId){
    return this.http.get<ItemDetail[]>(this.baseUrl + '/item-detail/get-ItemDetailByDepartmentId?departmentNameId='+departmentNameId+'')
  }
  getselectedItemDetailByDepartmentNameIdUnitIdItemCategoryId(pageNumber, pageSize, searchText,departmentNameId,unitId,itemCategoryId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('departmentNameId', departmentNameId.toString());
    params = params.append('unitId', unitId.toString());
    params = params.append('itemCategoryId', itemCategoryId.toString());
    return this.http.get<any>(this.baseUrl + '/item-detail/get-itemDetailByDepartmentIdUnitIdItemCategoryId', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ItemDetails = [...this.ItemDetails, ...response.body.items];
        this.ItemDetailPagination = response.body;
        return this.ItemDetailPagination;
      })
    );
   
  }

  // getselectedItemDetailByDepartmentNameIdUnitIdItemCategoryId(departmentNameId,unitId,itemCategoryId){
  //   return this.http.get<ItemDetail[]>(this.baseUrl + '/item-detail/get-itemDetailByDepartmentIdUnitIdItemCategoryId?departmentNameId='+departmentNameId+'&unitId='+unitId+'&itemCategoryId='+itemCategoryId+'')
  // }

  getselectedItemCategoryTypes(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-category-type/get-selectedItemCategoryTypes')
  }
  getselectedItemType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-type/get-selectedItemType')
  }
  getselectedTrades(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/trade/get-selectedTrades')
  }
  getselectedPresentStocks(departmentId){
    return this.http.get<any[]>(this.baseUrl + '/item-detail/get-presentStocks?departmentId='+departmentId)
  }
  
  getPartNoByDepartmentNameId(departmentId,itemCategoryId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-partnoByDepartmentNameId?departmentNameId='+departmentId+'&itemCategoryId='+itemCategoryId+'');
  }
  getItemNameById(id:number){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-itemNameByIdRequest?itemDetailId=' + id);
  }
  find(id: number) {
    return this.http.get<ItemDetail>(this.baseUrl + '/item-detail/get-ItemDetailDetail/' + id);
  }
  
  // getSelectedEquipmentByParameterRequest(unitId,departmentNameId,itemCategoryId){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/equipment-name/get-selectedEquipmentNameByParameterRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemCategoryId='+itemCategoryId+'');
  // }
  
  getSelectedEquipmentByParameterRequest(unitId,departmentNameId,itemCategoryId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-equipmentNameByUnitAndDepartmentAndCategoryIdRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemCategoryId='+itemCategoryId);
  }

  getSelectedEquipmentForSurveyByParameterRequest(unitId,departmentNameId,itemCategoryId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/issue-register/get-selectedEquipmentNameFromIssueRegisterByParameterRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemCategoryId='+itemCategoryId);
  }
  getSelectedEquipmentNameByIdRequest(unitId,departmentNameId,itemCategoryId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-equipmentByUnitAndDepartmentAndCategoryIdRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemCategoryId='+itemCategoryId+'');
  }
  findBaseSchoolNameId(id: number) {
    return this.http.get<ItemDetail>(this.baseUrl + '/base-School-name/get-baseSchoolNameDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/item-detail/update-ItemDetail/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/item-detail/save-ItemDetail', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/item-detail/delete-ItemDetail/'+id);
  }

}

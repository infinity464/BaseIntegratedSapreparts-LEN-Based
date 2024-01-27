import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IItemStorPagination, ItemStorPagination } from '../models/ItemStorPagination'
import { ItemStor } from '../models/ItemStor';
import { map } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { Acceptance } from '../models/Acceptance';
import { ItemDetail } from '../models/itemDetail';
@Injectable({
  providedIn: 'root'
})
export class ItemStorService {
  baseUrl = environment.apiUrl;
  ItemStors: ItemStor[] = [];
  ItemStorPagination = new ItemStorPagination();
  constructor(private http: HttpClient) { }

  getItemStors(pageNumber, pageSize, searchText, itemCategoryId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('itemCategoryId', itemCategoryId.toString());
    
    return this.http.get<IItemStorPagination>(this.baseUrl + '/item-stor/get-ItemStors', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ItemStors = [...this.ItemStors, ...response.body.items];
        this.ItemStorPagination = response.body;
        return this.ItemStorPagination;
      })
    );
  }
  getItemStorsList(pageNumber, pageSize, searchText, departmentNameId,unitId,itemCategoryId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('departmentNameId', departmentNameId.toString());
    params = params.append('unitId', unitId.toString());
    params = params.append('itemCategoryId', itemCategoryId.toString());

    return this.http.get<IItemStorPagination>(this.baseUrl + '/item-stor/get-itemStorListByParameterRequest', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ItemStors = [...this.ItemStors, ...response.body.items];
        this.ItemStorPagination = response.body;
        return this.ItemStorPagination;
      })
    );
  }
  // getSelectedSchoolName(baseNameId){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  // }

  getSelectedLocation(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/tools-location/get-selectedToolsLocations')
  }
  getSelectedConditionOfItem(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/condition-of-item/get-selectedConditionOfItem')
  }
  getSelectedDemandNo(unitId,departmentNameId,itemCategoryId,itemStatusId,sftStatus){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-selectedDemandNoByDepartmentIdUnitIdAndItemCategoryIdStatusRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemStatusId='+itemStatusId+'&status='+sftStatus+'&itemCategoryId='+itemCategoryId+'')
  }
  getSelectedDemandNoForStore(unitId,departmentNameId,itemCategoryId,storeStatus){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-selectedDemandNoForStoreByParameterRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&storeStatus='+storeStatus+'&itemCategoryId='+itemCategoryId+'')
  }
  getSelectedDemandNoByStatus(unitId,departmentNameId,itemCategoryId,itemStatusId,status){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-selectedDemandNoByItemStatus?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemCategoryId='+itemCategoryId+'&itemStatusId='+itemStatusId+'&status='+status)
  }
  findBaseSchoolNameId(id: number) {
    return this.http.get<ItemDetail>(this.baseUrl + '/base-School-name/get-baseSchoolNameDetail/' + id);
  }
  getselectedItemCategory(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-category/get-selectedItemCategory')
  }
  getSelectedSchoolName(baseNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  }
  getselectedDepartmentNames(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/department-name/get-selectedDepartmentNames')
  }
  getselectedDeno(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/deno/get-selectedDeno')
  }
  getselectedSparesCategory(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/spares-category/get-selectedSparesCategory')
  }
  getselectedConditionOfItem(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/condition-of-item/get-selectedConditionOfItem')
  }
  getselectedToolsLocations(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/tools-location/get-selectedToolsLocations')
  }
  getselectedLifeLimitItem(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/life-limit-tem/get-selectedLifeLimitItems')
  }
  getselectedAcctStore(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/acct-store/get-selectedAcctStore')
  }
  getselectedServiceLifeType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/service-life-type/get-selectedServiceLifeType')
  }
  
  getselectedEndLifeType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/endlife-type/get-selectedEndLifeType')
  }
  getselectedOverhaulingTypes(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/overhauling-type/get-selectedOverhaulingTypes')
  }
  
  partnoFromAcceptanceByDepartmentName(departmentNameId,sparesCategoryId) {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/acceptance/get-partnoFromAcceptanceByDepartmentNameId?departmentNameId='+departmentNameId+'&sparesCategoryId='+sparesCategoryId);
  }

  partnoFromAcceptanceForUpdateByDepartmentName(departmentNameId,sparesCategoryId) {
    return this.http.get<SelectedModel[]>(this.baseUrl + '/acceptance/get-partnoFromAcceptanceForUpdateByDepartmentNameId?departmentNameId='+departmentNameId+'&sparesCategoryId='+sparesCategoryId);
  }

  getacceptanceById(acceptanceId) {
    return this.http.get<Acceptance[]>(this.baseUrl + '/acceptance/get-acceptanceById?acceptanceId='+acceptanceId);
  }

  find(id: number) {
    return this.http.get<ItemStor>(this.baseUrl + '/item-stor/get-ItemStorDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/item-stor/update-ItemStor/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/item-stor/save-ItemStor', model);
  } 
  previousItemsubmit(model: any) {
    return this.http.post(this.baseUrl + '/item-stor/save-PreviousItemStor', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/item-stor/delete-ItemStor/'+id);
  }

}

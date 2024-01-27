import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IPreviousItemStorePagination, PreviousItemStorePagination } from '../models/PreviousItemStorePagination'
import { PreviousItemStore } from '../models/PreviousItemStore';
import { map } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class PreviousItemStoreService {
  baseUrl = environment.apiUrl;
  PreviousItemStores: PreviousItemStore[] = [];
  PreviousItemStorePagination = new PreviousItemStorePagination();
  constructor(private http: HttpClient) { }


  getPreviousItemStores(pageNumber, pageSize, searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IPreviousItemStorePagination>(this.baseUrl + '/previous-item-store/get-PreviousItemStores', { observe: 'response', params })
    .pipe(
      map(response => {
        this.PreviousItemStores = [...this.PreviousItemStores, ...response.body.items];
        this.PreviousItemStorePagination = response.body;
        return this.PreviousItemStorePagination;
      })
    );
   
  }
  getPreviousItemStoreListByDepartmentId(departmentNameId){
    return this.http.get<PreviousItemStore[]>(this.baseUrl + '/previous-item-store/get-selectedPreviousItemStoreListByDepartmentId?departmentNameId='+departmentNameId)
  }
  getselectedItemDetails(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-selectedItemDetails')
  }
  getselectedToolsType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/tools-type/get-selectedToolsType')
  }
  getselectedToolsBoxNames(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/toolsbox-name/get-selectedToolsBoxNames')
  }
  getselectedToolsLocations(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/tools-location/get-selectedToolsLocations')
  }
  getselectedDeno(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/deno/get-selectedDeno')
  }
  getselectedDepartmentNames(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/department-name/get-selectedDepartmentNames')
  }
  getselectedItemCategory(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-category/get-selectedItemCategory')
  }
  getSelectedSchoolName(baseNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  }
  getselectedSparesCategory(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/spares-category/get-selectedSparesCategory')
  }
  getselectedServiceLifeType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/service-life-type/get-selectedServiceLifeType')
  }
  getselectedEndLifeType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/endlife-type/get-selectedEndLifeType')
  }
  getselectedAcctStore(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/acct-store/get-selectedAcctStore')
  }
  getselectedOverhaulingTypes(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/overhauling-type/get-selectedOverhaulingTypes')
  }
  getselectedRetirementTypes(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/retirement-type/get-selectedRetirementTypes')
  }


  find(id: number) {
    return this.http.get<PreviousItemStore>(this.baseUrl + '/previous-item-store/get-PreviousItemStoreDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/previous-item-store/update-PreviousItemStore/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/previous-item-store/save-PreviousItemStore', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/previous-item-store/delete-PreviousItemStore/'+id);
  }

}

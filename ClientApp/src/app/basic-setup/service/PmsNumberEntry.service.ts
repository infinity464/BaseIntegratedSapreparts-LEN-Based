import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IPmsNumberEntryPagination,PmsNumberEntryPagination } from '../models/PmsNumberEntryPagination'
import { PmsNumberEntry } from '../models/PmsNumberEntry';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class PmsNumberEntryService {
  baseUrl = environment.apiUrl;
  PmsNumberEntrys: PmsNumberEntry[] = [];
  PmsNumberEntryPagination = new PmsNumberEntryPagination();
  constructor(private http: HttpClient) { }

  getPmsNumberEntrysList(pageNumber, pageSize, searchText,unitId,departmentNameId,itemDetailId) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('unitId', unitId.toString());
    params = params.append('departmentNameId', departmentNameId.toString());
    params = params.append('itemDetailId', itemDetailId.toString());

    return this.http.get<IPmsNumberEntryPagination>(this.baseUrl + '/pms-number-entry/get-PmsNumberEntriesByUnitIdDeptId', { observe: 'response', params })
    .pipe(
      map(response => {
        this.PmsNumberEntrys = [...this.PmsNumberEntrys, ...response.body.items];
        this.PmsNumberEntryPagination = response.body;
        return this.PmsNumberEntryPagination;
      })
    );
   
  }

  getPmsNumberEntrys(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IPmsNumberEntryPagination>(this.baseUrl + '/pms-number-entry/get-PmsNumberEntries', { observe: 'response', params })
    .pipe(
      map(response => {
        this.PmsNumberEntrys = [...this.PmsNumberEntrys, ...response.body.items];
        this.PmsNumberEntryPagination = response.body;
        return this.PmsNumberEntryPagination;
      })
    );
   
  }
  getPmsNumberEntryByDepartment(departmentNameId:number){
    return this.http.get<PmsNumberEntry[]>(this.baseUrl + '/pms-number-entry/get-pmsNumberEntryByDepartment?departmentNameId='+departmentNameId);
   }
  getSelectedSchoolName(baseNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  }
  // getEquipmentName(){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/equipment-name/get-selectedEquipmentName');
  //  }
   getItemDetail(departmentNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-ItemNameByItemCategoryDepartmentNameId?departmentNameId='+departmentNameId);
   }
  find(id: number) {
    return this.http.get<PmsNumberEntry>(this.baseUrl + '/pms-number-entry/get-PmsNumberEntryDetail/' + id);
  }

  findPmsNo(id: number) {
    return this.http.get<PmsNumberEntry>(this.baseUrl + '/pms-number-entry/get-PmsNumberEntryDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/pms-number-entry/update-PmsNumberEntry/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/pms-number-entry/save-PmsNumberEntry', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/pms-number-entry/delete-PmsNumberEntry/'+id);
  }

}

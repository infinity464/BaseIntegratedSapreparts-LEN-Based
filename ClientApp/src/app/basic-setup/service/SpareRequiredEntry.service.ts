import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {ISpareRequiredEntryPagination,SpareRequiredEntryPagination } from '../models/SpareRequiredEntryPagination'
import { SpareRequiredEntry } from '../models/SpareRequiredEntry';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class SpareRequiredEntryService {
  baseUrl = environment.apiUrl;
  SpareRequiredEntrys: SpareRequiredEntry[] = [];
  SpareRequiredEntryPagination = new SpareRequiredEntryPagination();
  constructor(private http: HttpClient) { }

  getSpareRequiredEntrys(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<ISpareRequiredEntryPagination>(this.baseUrl + '/spare-required-entry/get-SpareRequiredEntries', { observe: 'response', params })
    .pipe(
      map(response => {
        this.SpareRequiredEntrys = [...this.SpareRequiredEntrys, ...response.body.items];
        this.SpareRequiredEntryPagination = response.body;
        return this.SpareRequiredEntryPagination;
      })
    );
   
  }
  getSpareRequiredEntryByDepartment(departmentNameId:number){
    return this.http.get<SpareRequiredEntry[]>(this.baseUrl + '/spare-required-entry/get-spareRequiredEntryByDepartment?departmentNameId='+departmentNameId);
   }
  getSelectedSchoolName(baseNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  }
  getItemDetail(departmentNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-ItemNameByItemCategoryAndDepartmentNameId?departmentNameId='+departmentNameId);
   }
   getJobDescription(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/job-description-entry-pms/get-selectedJobDescriptionEntryPmses');
   }
   getJicByJobDescriptionEntryPmsId(id:number){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/job-description-entry-pms/get-selectedJICByJobDescriptionEntryPmsId?jobDescriptionEntryPmsId=' + id);
  }
  find(id: number) {
    return this.http.get<SpareRequiredEntry>(this.baseUrl + '/spare-required-entry/get-SpareRequiredEntryDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/spare-required-entry/update-SpareRequiredEntry/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/spare-required-entry/save-SpareRequiredEntry', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/spare-required-entry/delete-SpareRequiredEntry/'+id);
  }

}

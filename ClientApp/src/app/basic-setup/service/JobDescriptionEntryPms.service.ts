import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IJobDescriptionEntryPmsPagination,JobDescriptionEntryPmsPagination } from '../models/JobDescriptionEntryPmsPagination'
import { JobDescriptionEntryPms } from '../models/JobDescriptionEntryPms';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class JobDescriptionEntryPmsService {
  baseUrl = environment.apiUrl;
  JobDescriptionEntryPmses: JobDescriptionEntryPms[] = [];
  JobDescriptionEntryPmsPagination = new JobDescriptionEntryPmsPagination();
  constructor(private http: HttpClient) { }

  // getByWhoms(pageNumber, pageSize, searchText) { 

  //   let params = new HttpParams();

  //   params = params.append('searchText', searchText.toString());
  //   params = params.append('pageNumber', pageNumber.toString());
  //   params = params.append('pageSize', pageSize.toString());

    
  //   return this.http.get<IByWhomPagination>(this.baseUrl + '/job-description-entry-pms/get-JobDescriptionEntryPmses', { observe: 'response', params })
  //   .pipe(
  //     map(response => {
  //       this.ByWhoms = [...this.ByWhoms, ...response.body.items];
  //       this.ByWhomPagination = response.body;
  //       return this.ByWhomPagination;
  //     })
  //   );
   
  // }
  getJobDescriptionEntryPmss(pageNumber, pageSize, searchText, unitId, departmentNameId) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('unitId', unitId.toString());
    params = params.append('departmentNameId', departmentNameId.toString());

    
    return this.http.get<IJobDescriptionEntryPmsPagination>(this.baseUrl + '/job-description-entry-pms/get-JobDescriptionEntryPmses', { observe: 'response', params })
    .pipe(
      map(response => {
        this.JobDescriptionEntryPmses = [...this.JobDescriptionEntryPmses, ...response.body.items];
        this.JobDescriptionEntryPmsPagination = response.body;
        return this.JobDescriptionEntryPmsPagination;
      })
    );
   
  }

  getJobDescriptionEntryPmsByUnitAndDept(pageNumber, pageSize, searchText, unitId, departmentNameId,pmsNumberEntryId) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('unitId', unitId.toString());
    params = params.append('departmentNameId', departmentNameId.toString());
    params = params.append('pmsNumberEntryId', pmsNumberEntryId.toString());

    return this.http.get<IJobDescriptionEntryPmsPagination>(this.baseUrl + '/job-description-entry-pms/get-jobDescriptionEntryByUnitAndDeptandPmsNumberId', { observe: 'response', params })
    .pipe(
      map(response => {
        this.JobDescriptionEntryPmses = [...this.JobDescriptionEntryPmses, ...response.body.items];
        this.JobDescriptionEntryPmsPagination = response.body;
        return this.JobDescriptionEntryPmsPagination;
      })
    );
   
  }

  //spare-required-status/get-selectedSpareRequiredStatus

  getSelectedSpareRequiredStatus(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/spare-required-status/get-selectedSpareRequiredStatus');
  }

  getPmsNumberEntryByDepartment(departmentNameId:number){
    return this.http.get<JobDescriptionEntryPms[]>(this.baseUrl + '/job-description-entry-pms/get-jobDescriptionEntryPmsByDepartment?departmentNameId='+departmentNameId);
   }
  getSelectedSchoolName(baseNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  }
  getPmsNumberEntry(unitId,departmentNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/pms-number-entry/get-selectedItemNameFromPmsNumberEntry?unitId='+unitId+'&departmentNameId='+departmentNameId);
   }
   getPmsNoByPmsNumberEntryId(id:number){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/pms-number-entry/get-selectedPmsNoByPmsNumberEntryId?pmsNumberEntryId=' + id);
  }
   getMaintenanceTypes(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/maintenance-type/get-selectedMaintenanceTypes');
   }
   getMaintenanceCategory(maintenanceTypeId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/maintenance-category/get-selectedCategoryByDepartmentNameIdAndMaintenanceTypeId?maintenanceTypeId='+maintenanceTypeId);
   }
   getByWhom(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/by-whom/get-selectedByWhoms');
   }
  find(id: number) {
    return this.http.get<JobDescriptionEntryPms>(this.baseUrl + '/job-description-entry-pms/get-JobDescriptionEntryPmsDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/job-description-entry-pms/update-JobDescriptionEntryPms/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/job-description-entry-pms/save-JobDescriptionEntryPms', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/job-description-entry-pms/delete-JobDescriptionEntryPms/'+id);
  }

}

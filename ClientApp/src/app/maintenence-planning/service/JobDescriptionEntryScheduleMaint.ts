import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IJobDescriptionScheduleMaintenencePagination, JobDescriptionScheduleMaintenencePagination } from '../models/JobDescriptionScheduleMaintenencePagination'
import { JobDescriptionScheduleMaintenence } from '../models/JobDescriptionScheduleMaintenence';
import { map } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class JobDescriptionEntryScheduleMaintService {
  baseUrl = environment.apiUrl;
  JobDescriptionScheduleMaintenences: JobDescriptionScheduleMaintenence[] = [];
  JobDescriptionScheduleMaintenencePagination = new JobDescriptionScheduleMaintenencePagination();
  constructor(private http: HttpClient) { }


  getJobDescriptionScheduleMaintenences(pageNumber, pageSize, searchText,unitId,departmentNameId,itemDetailId) {
    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('unitId', unitId.toString());
    params = params.append('departmentNameId', departmentNameId.toString());
    params = params.append('itemDetailId', itemDetailId.toString()); 
    return this.http.get<IJobDescriptionScheduleMaintenencePagination>(this.baseUrl + '/job-description-schedule-maintenence/get-jobDescriptionScheduleMaintenences', { observe: 'response', params })
    .pipe(
      map(response => {
        this.JobDescriptionScheduleMaintenences = [...this.JobDescriptionScheduleMaintenences, ...response.body.items];
        this.JobDescriptionScheduleMaintenencePagination = response.body;
        return this.JobDescriptionScheduleMaintenencePagination;
      })
    );
   
  }

    getSelectedScheduleMaintenenceType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/schedule-maintenence-type/get-selectedScheduleMaintenenceType')
  }

  getSelectedFiscalYear(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/fiscal-year/get-selectedFiscalYear')
  }
  getSelectedJobCategoryMaintenence(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/job-category-maintenence/get-selectedJobCategoryMaintenence')
  }
//   getselectedRank(){job-category-maintenence/get-selectedJobCategoryMaintenence
//     return this.http.get<SelectedModel[]>(this.baseUrl + '/rank/get-selectedRanks')
//   }
//   getselectedOfficersStatuses(){
//     return this.http.get<SelectedModel[]>(this.baseUrl + '/officers-status/get-selectedOfficersStatuses')
//   }
//   getselectedDepartmentNames(){
//     return this.http.get<SelectedModel[]>(this.baseUrl + '/department-name/get-selectedDepartmentNames')
//   }
//   getSelectedSchoolName(baseNameId){
//     return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
//   }
  
//  getJobDescriptionScheduleMaintenenceListByDepartmentNameId( departmentNameId:number){
//   return this.http.get<JobDescriptionScheduleMaintenence[]>(this.baseUrl + '/training-crew/get-JobDescriptionScheduleMaintenenceListByDepartmentNameId?departmentNameId='+departmentNameId);
//  issue-register/get-selectedEquipmentNameFromIssueRegisterByParameterRequest?unitId=14&departmentNameId=29&itemCategoryId=1
  
getSelectedEquipmentNameFromIssueRegisterByParameter(unitId,departmentNameId,itemCategoryId){
  return this.http.get<SelectedModel[]>(this.baseUrl + '/issue-register/get-selectedEquipmentNameFromIssueRegisterByParameterRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemCategoryId='+itemCategoryId+'')
}
  find(id: number) {
    return this.http.get<JobDescriptionScheduleMaintenence>(this.baseUrl + '/job-description-schedule-maintenence/get-jobDescriptionScheduleMaintenenceDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/job-description-schedule-maintenence/update-jobDescriptionScheduleMaintenence/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/job-description-schedule-maintenence/save-jobDescriptionScheduleMaintenence', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/job-description-schedule-maintenence/delete-jobDescriptionScheduleMaintenence/'+id);
  }

}

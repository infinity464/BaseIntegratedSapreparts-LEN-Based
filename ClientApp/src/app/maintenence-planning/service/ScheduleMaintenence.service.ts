import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IScheduleMaintenencePagination, ScheduleMaintenencePagination } from '../models/ScheduleMaintenencePagination'
import { ScheduleMaintenence } from '../models/ScheduleMaintenence';
import { map } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { IJobDescriptionScheduleMaintenencePagination, JobDescriptionScheduleMaintenencePagination } from '../models/JobDescriptionScheduleMaintenencePagination';
import { JobDescriptionScheduleMaintenence } from '../models/JobDescriptionScheduleMaintenence';
@Injectable({
  providedIn: 'root'
})
export class ScheduleMaintenenceService {
  baseUrl = environment.apiUrl;
  ScheduleMaintenences: ScheduleMaintenence[] = [];
  ScheduleMaintenencePagination = new ScheduleMaintenencePagination();
  JobDescriptionScheduleMaintenences: JobDescriptionScheduleMaintenence[] = [];
  JobDescriptionScheduleMaintenencePagination = new JobDescriptionScheduleMaintenencePagination();
  constructor(private http: HttpClient) { }


  getScheduleMaintenences(pageNumber, pageSize, searchText,unitId,departmentNameId,issueRegisterId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('unitId', unitId.toString());
    params = params.append('departmentNameId', departmentNameId.toString());
    params = params.append('issueRegisterId', issueRegisterId.toString());

    return this.http.get<IScheduleMaintenencePagination>(this.baseUrl + '/schedule-maintenance/get-scheduleMaintenances', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ScheduleMaintenences = [...this.ScheduleMaintenences, ...response.body.items];
        this.ScheduleMaintenencePagination = response.body;
        return this.ScheduleMaintenencePagination;
      })
    );
   
  }

  
  getScheduleMaintenencesForDept(pageNumber, pageSize, searchText,unitId,departmentNameId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('unitId', unitId.toString());
    params = params.append('departmentNameId', departmentNameId.toString());

    return this.http.get<IScheduleMaintenencePagination>(this.baseUrl + '/schedule-maintenance/get-scheduleMaintenancesListByDept', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ScheduleMaintenences = [...this.ScheduleMaintenences, ...response.body.items];
        this.ScheduleMaintenencePagination = response.body;
        return this.ScheduleMaintenencePagination;
      })
    );
   
  }

  getJobDescriptionScheduleMaintenences(pageNumber, pageSize, searchText,unitId,departmentNameId,issueRegisterId,scheduleMaintenenceTypeId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('unitId', unitId.toString());
    params = params.append('departmentNameId', departmentNameId.toString());
    params = params.append('issueRegisterId', issueRegisterId.toString());
    params = params.append('scheduleMaintenenceTypeId', scheduleMaintenenceTypeId.toString());

    return this.http.get<IJobDescriptionScheduleMaintenencePagination>(this.baseUrl + '/job-description-schedule-maintenence/get-jobDescriptionScheduleMaintenencesForMaintanence', { observe: 'response', params })
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
  getSelectedEquipmentForScheduleByParameterRequest(unitId,departmentNameId,issueRegisterId,scheduleMaintenenceTypeId,fiscalYearId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/job-description-schedule-maintenence/get-selectedJobDescriptionForScheduleByParameterRequest?unitId='+unitId+'&departmentameId='+departmentNameId+'&issueRegisterId='+issueRegisterId+'&scheduleMaintenenceTypeId='+scheduleMaintenenceTypeId+'&fiscalYearId='+fiscalYearId+'')
  }
  getSelectedFiscalYear(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/fiscal-year/get-selectedFiscalYear')
  }


  getSelectedProgress(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/progres/get-selectedPriorities')
  }

  getSelectedJobCategoryMaintenence(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/job-category-maintenence/get-selectedJobCategoryMaintenence')
  }

getSelectedEquipmentNameFromIssueRegisterByParameter(unitId,departmentNameId,itemCategoryId){
  return this.http.get<SelectedModel[]>(this.baseUrl + '/issue-register/get-selectedEquipmentNameFromIssueRegisterByParameterRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemCategoryId='+itemCategoryId+'')
}

findBaseSchoolNameId(id: number) {
  return this.http.get<any>(this.baseUrl + '/base-School-name/get-baseSchoolNameDetail/' + id);
}
completeSchedule(id: number,model: any){
  return this.http.post(this.baseUrl + '/schedule-maintenance/completed-schedule-maintenence/'+id, model);
}
completedScheduleMaintenence(id: number,model: any) {
  return this.http.post(this.baseUrl + '/schedule-maintenance/completed-schedule-maintenence/'+id, model);
}

  find(id: number) {
    return this.http.get<ScheduleMaintenence>(this.baseUrl + '/schedule-maintenance/get-scheduleMaintenanceDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/schedule-maintenance/update-scheduleMaintenance/'+id, model);
  }

  updateScheduleMaintenence(id: number,model: any) {
    return this.http.put(this.baseUrl + '/schedule-maintenance/update-scheduleMaintenance/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/schedule-maintenance/save-scheduleMaintenance', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/schedule-maintenance/delete-scheduleMaintenance/'+id);
  }

}

import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {ICorrectiveMaintenencePagination,CorrectiveMaintenencePagination } from '../models/CorrectiveMaintenencePagination'
import { CorrectiveMaintenence } from '../models/CorrectiveMaintenence';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class CorrectiveMaintenenceService {
  baseUrl = environment.apiUrl;
  CorrectiveMaintenences: CorrectiveMaintenence[] = [];
  CorrectiveMaintenencePagination = new CorrectiveMaintenencePagination();
  constructor(private http: HttpClient) { }

  getCorrectiveMaintenences(pageNumber, pageSize, searchText,unitId,departmentNameId,issueRegisterId) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('unitId', unitId.toString());
    params = params.append('departmentNameId', departmentNameId.toString());
    params = params.append('issueRegisterId', issueRegisterId.toString());

    return this.http.get<ICorrectiveMaintenencePagination>(this.baseUrl + '/corrective-maintenence/get-correctiveMaintenences', { observe: 'response', params })
    .pipe(
      map(response => {
        this.CorrectiveMaintenences = [...this.CorrectiveMaintenences, ...response.body.items];
        this.CorrectiveMaintenencePagination = response.body;
        return this.CorrectiveMaintenencePagination;
      })
    );
   
  }
  completedCorrectiveMaintenence(id: number,model: any) {
    return this.http.post(this.baseUrl + '/corrective-maintenence/completed-corrective-maintenence/'+id, model);
  }
  

  find(id: number) {
    return this.http.get<CorrectiveMaintenence>(this.baseUrl + '/corrective-maintenence/get-correctiveMaintenenceDetail/' + id);
  }


  getSelectedPriority(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/priority/get-selectedPriorities')
  }
  getSelectedMaintenenceAuthority(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/maintenance-authority/get-selectedMaintenanceAuthoritys')
  }
  getSelectedByWhoms(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/by-whom/get-selectedByWhoms')
  }
  getSelectedStatusOfJob(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/status-of-job/get-selectedStatusOfJobs')
  }
  getSpareStockByParameterSpRequest(unitId,departmentNameId,equipmentNameId){
    return this.http.get<any[]>(this.baseUrl + '/corrective-maintenence/get-spareStockByParameterSpRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&equipmentNameId='+equipmentNameId+'')
  }
  // corrective-maintenence/get-spareStockByParameterSpRequest?unitId=14&departmentNameId=29&itemCategoryId=2
  getSelectedEquipmentNameByParameterRequest(unitId,departmentNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/issue-register/get-selectedEquipmentNameByParameterRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'')
  }

  getSelectedSpareRequisition(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/spare-requisition/get-selectedSpareRequisition')
  }

  getSelectedEquipmentForScheduleByParameterRequest(unitId,departmentNameId,issueRegisterId,scheduleMaintenenceTypeId,fiscalYearId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/job-description-schedule-maintenence/get-selectedJobDescriptionForScheduleByParameterRequest?unitId='+unitId+'&departmentameId='+departmentNameId+'&issueRegisterId='+issueRegisterId+'&scheduleMaintenenceTypeId='+scheduleMaintenenceTypeId+'&fiscalYearId='+fiscalYearId+'')
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/corrective-maintenence/update-correctiveMaintenence/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/corrective-maintenence/save-correctiveMaintenence', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/corrective-maintenence/delete-correctiveMaintenence/'+id);
  }

}

import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IPreventiveMaintenancePagination, PreventiveMaintenancePagination } from '../models/PreventiveMaintenancePagination'
import { PreventiveMaintenance } from '../models/PreventiveMaintenance';
import { map } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class PreventiveMaintenanceService {
  baseUrl = environment.apiUrl;
  PreventiveMaintenances: PreventiveMaintenance[] = [];
  PreventiveMaintenancePagination = new PreventiveMaintenancePagination();
  constructor(private http: HttpClient) { }


  getPreventiveMaintenances(pageNumber, pageSize, searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    return this.http.get<IPreventiveMaintenancePagination>(this.baseUrl + '/preventive-maintenance/get-PreventiveMaintenances', { observe: 'response', params })
    .pipe(
      map(response => {
        this.PreventiveMaintenances = [...this.PreventiveMaintenances, ...response.body.items];
        this.PreventiveMaintenancePagination = response.body;
        return this.PreventiveMaintenancePagination;
      })
    );
   
  }
  getMaintenanceCategory(maintenanceTypeId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/maintenance-category/get-selectedCategoryByDepartmentNameIdAndMaintenanceTypeId?maintenanceTypeId='+maintenanceTypeId);
   }
  getMaintenanceTypes(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/maintenance-type/get-selectedMaintenanceTypes');
   }
//autocomplete for By PartNo   
getPartNo(partNo){
  return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-autocompletePartNoByNameForSpares?partNo='+partNo+'')
    .pipe(
      map((response:[]) => response.map(item => item))
    )
}

completedPreventiveMaintenence(id: number,model: any) {
  return this.http.post(this.baseUrl + '/preventive-maintenance/completed-preventive-maintenence/'+id, model);
}

getPmsNoByItemDetailId(itemDetailId){
  return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-selectedPmsNoByItemDetailId?itemDetailId='+itemDetailId+'')
}
getPreventiveMaintenanceyByParameterRequest(unitId, departmentNameId, maintenenceTypeId, maintenenceCategoryId,itemDetailId){
  return this.http.get<any[]>(this.baseUrl + '/preventive-maintenance/get-preventiveMaintenanceListByParameterRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&maintenenceTypeId='+maintenenceTypeId+'&maintenanceCategoryId='+maintenenceCategoryId+'&itemDetailId='+itemDetailId+'');
 }

 getPmsJobDescriptionListForPreventive(unitId, departmentNameId, maintenenceTypeId, maintenenceCategoryId,itemDetailId){
  return this.http.get<any[]>(this.baseUrl + '/job-description-entry-pms/get-jobDescriptionEntryPmsListForPreventive?unitId='+unitId+'&departmentNameId='+departmentNameId+'&maintenenceTypeId='+maintenenceTypeId+'&maintenenceCategoryId='+maintenenceCategoryId+'&itemDetailId='+itemDetailId+'');
 }

 getPreventiveList(departmentNameId,unitId){
  return this.http.get<PreventiveMaintenance[]>(this.baseUrl + '/preventive-maintenance/get-preventiveListByDepartmentNameId?departmentNameId='+departmentNameId+'&unitId='+unitId+'');
}

    getJobDescriptionEntryPmsIdByParameterRequest(unitId, departmentNameId, maintenenceTypeId, maintenenceCategoryId,itemDetailId){
      return this.http.get<SelectedModel[]>(this.baseUrl + '/job-description-entry-pms/get-selectedJobDescriptionEntryPmsIdByParameterRequest?unitId='+unitId+'&departmentNameId='+departmentNameId+'&maintenenceTypeId='+maintenenceTypeId+'&maintenenceCategoryId='+maintenenceCategoryId+'&itemDetailId='+itemDetailId+'')
    }
    getJICByParameterRequest(unitId, departmentNameId,itemDetailId){
      return this.http.get<SelectedModel[]>(this.baseUrl + '/job-description-entry-pms/get-jicByUnitAndDepartmentAndItemDetailId?unitId='+unitId+'&departmentNameId='+departmentNameId+'&itemDetailId='+itemDetailId+'')
    }
    
  getPreventiveMaintenanceyByDepartment(departmentNameId:number){
    return this.http.get<PreventiveMaintenance[]>(this.baseUrl + '/preventive-maintenance/get-preventiveMaintenanceByDepartment?departmentNameId='+departmentNameId);
   }
  getselectedDepartmentNames(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/department-name/get-selectedDepartmentNames')
  }
  getselectedMaintenanceCategory(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/maintenance-category/get-selectedMaintenanceCategorys')
  }
  getselectedSubCategory(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/maintenance-sub-category/get-selectedMaintenanceSubCategorys')
  }
  getSelectedSchoolName(baseNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  }

  find(id: number) {
    return this.http.get<PreventiveMaintenance>(this.baseUrl + '/preventive-maintenance/get-PreventiveMaintenanceDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/preventive-maintenance/update-PreventiveMaintenance/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/preventive-maintenance/save-PreventiveMaintenance', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/preventive-maintenance/delete-PreventiveMaintenance/'+id);
  }

}

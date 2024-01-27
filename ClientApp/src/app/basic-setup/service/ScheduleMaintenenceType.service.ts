import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IScheduleMaintenenceTypePagination,ScheduleMaintenenceTypePagination } from '../models/ScheduleMaintenenceTypePagination'
import { ScheduleMaintenenceType } from '../models/ScheduleMaintenenceType';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ScheduleMaintenenceTypeService {
  baseUrl = environment.apiUrl;
  ScheduleMaintenenceTypes: ScheduleMaintenenceType[] = [];
  ScheduleMaintenenceTypePagination = new ScheduleMaintenenceTypePagination();
  constructor(private http: HttpClient) { }

  getScheduleMaintenenceTypes(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IScheduleMaintenenceTypePagination>(this.baseUrl + '/schedule-maintenence-type/get-scheduleMaintenenceTypes', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ScheduleMaintenenceTypes = [...this.ScheduleMaintenenceTypes, ...response.body.items];
        this.ScheduleMaintenenceTypePagination = response.body;
        return this.ScheduleMaintenenceTypePagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<ScheduleMaintenenceType>(this.baseUrl + '/schedule-maintenence-type/get-scheduleMaintenenceTypeDetail/' + id);
  }


  // getselecteddivision(){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/ScheduleMaintenenceType/get-selectedScheduleMaintenenceType')
  // }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/schedule-maintenence-type/update-scheduleMaintenenceType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/schedule-maintenence-type/save-scheduleMaintenenceType', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/schedule-maintenence-type/delete-scheduleMaintenenceType/'+id);
  }

}

import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IDemandTypePagination,DemandTypePagination } from '../models/DemandTypePagination'
import { DemandType } from '../models/DemandType';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class DemandTypeService {
  baseUrl = environment.apiUrl;
  DemandTypes: DemandType[] = [];
  DemandTypePagination = new DemandTypePagination();
  constructor(private http: HttpClient) { }

  getDemandTypes(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IDemandTypePagination>(this.baseUrl + '/demand-type/get-DemandTypes', { observe: 'response', params })
    .pipe(
      map(response => {
        this.DemandTypes = [...this.DemandTypes, ...response.body.items];
        this.DemandTypePagination = response.body;
        return this.DemandTypePagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<DemandType>(this.baseUrl + '/demand-type/get-DemandTypeDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/demand-type/update-DemandType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/demand-type/save-DemandType', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/demand-type/delete-DemandType/'+id);
  }

}

import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IReturnStatusPagination,ReturnStatusPagination } from '../models/ReturnStatusPagination'
import { ReturnStatus } from '../models/ReturnStatus';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ReturnStatusService {
  baseUrl = environment.apiUrl;
  ReturnStatuses: ReturnStatus[] = [];
  ReturnStatusPagination = new ReturnStatusPagination();
  constructor(private http: HttpClient) { }

  getReturnStatuses(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IReturnStatusPagination>(this.baseUrl + '/return-status/get-returnStatuses', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ReturnStatuses = [...this.ReturnStatuses, ...response.body.items];
        this.ReturnStatusPagination = response.body;
        return this.ReturnStatusPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<ReturnStatus>(this.baseUrl + '/return-status/get-returnStatusDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/return-status/update-returnStatus/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/return-status/save-returnStatus', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/return-status/delete-returnStatus/'+id);
  }

}

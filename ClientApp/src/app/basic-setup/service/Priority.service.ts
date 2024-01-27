import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IPriorityPagination,PriorityPagination } from '../models/PriorityPagination'
import { Priority } from '../models/Priority';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class PriorityService {
  baseUrl = environment.apiUrl;
  Prioritys: Priority[] = [];
  PriorityPagination = new PriorityPagination();
  constructor(private http: HttpClient) { }

  getPrioritys(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IPriorityPagination>(this.baseUrl + '/priority/get-Priorities', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Prioritys = [...this.Prioritys, ...response.body.items];
        this.PriorityPagination = response.body;
        return this.PriorityPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<Priority>(this.baseUrl + '/priority/get-PriorityDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/priority/update-Priority/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/priority/save-Priority', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/priority/delete-Priority/'+id);
  }

}

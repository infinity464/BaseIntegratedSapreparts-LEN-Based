import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IStatusOfJobPagination,StatusOfJobPagination } from '../models/StatusOfJobPagination'
import { StatusOfJob } from '../models/StatusOfJob';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class StatusOfJobService {
  baseUrl = environment.apiUrl;
  StatusOfJobs: StatusOfJob[] = [];
  StatusOfJobPagination = new StatusOfJobPagination();
  constructor(private http: HttpClient) { }

  getStatusOfJobs(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IStatusOfJobPagination>(this.baseUrl + '/status-of-job/get-StatusOfJobs', { observe: 'response', params })
    .pipe(
      map(response => {
        this.StatusOfJobs = [...this.StatusOfJobs, ...response.body.items];
        this.StatusOfJobPagination = response.body;
        return this.StatusOfJobPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<StatusOfJob>(this.baseUrl + '/status-of-job/get-StatusOfJobDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/status-of-job/update-StatusOfJob/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/status-of-job/save-StatusOfJob', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/status-of-job/delete-StatusOfJob/'+id);
  }

}

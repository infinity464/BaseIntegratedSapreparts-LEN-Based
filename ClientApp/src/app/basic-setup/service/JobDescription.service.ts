import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IJobDescriptionPagination,JobDescriptionPagination } from '../models/JobDescriptionPagination'
import { JobDescription } from '../models/JobDescription';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class JobDescriptionService {
  baseUrl = environment.apiUrl;
  JobDescriptions: JobDescription[] = [];
  JobDescriptionPagination = new JobDescriptionPagination();
  constructor(private http: HttpClient) { }

  getJobDescriptions(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IJobDescriptionPagination>(this.baseUrl + '/job-description/get-JobDescriptions', { observe: 'response', params })
    .pipe(
      map(response => {
        this.JobDescriptions = [...this.JobDescriptions, ...response.body.items];
        this.JobDescriptionPagination = response.body;
        return this.JobDescriptionPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<JobDescription>(this.baseUrl + '/job-description/get-JobDescriptionDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/job-description/update-JobDescription/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/job-description/save-JobDescription', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/job-description/delete-JobDescription/'+id);
  }

}

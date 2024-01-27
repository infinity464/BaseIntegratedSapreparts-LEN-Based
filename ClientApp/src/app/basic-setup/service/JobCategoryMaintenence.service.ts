import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IJobCategoryMaintenencePagination,JobCategoryMaintenencePagination } from '../models/JobCategoryMaintenencePagination'
import { JobCategoryMaintenence } from '../models/JobCategoryMaintenence';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class JobCategoryMaintenenceService {
  baseUrl = environment.apiUrl;
  JobCategoryMaintenences: JobCategoryMaintenence[] = [];
  JobCategoryMaintenencePagination = new JobCategoryMaintenencePagination();
  constructor(private http: HttpClient) { }

  getJobCategoryMaintenences(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IJobCategoryMaintenencePagination>(this.baseUrl + '/job-category-maintenence/get-jobCategoryMaintenences', { observe: 'response', params })
    .pipe(
      map(response => {
        this.JobCategoryMaintenences = [...this.JobCategoryMaintenences, ...response.body.items];
        this.JobCategoryMaintenencePagination = response.body;
        return this.JobCategoryMaintenencePagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<JobCategoryMaintenence>(this.baseUrl + '/job-category-maintenence/get-jobCategoryMaintenenceDetail/' + id);
  }


  // getselecteddivision(){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/JobCategoryMaintenence/get-selectedDeno')
  // }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/job-category-maintenence/update-jobCategoryMaintenence/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/job-category-maintenence/save-jobCategoryMaintenence', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/job-category-maintenence/delete-jobCategoryMaintenence/'+id);
  }

}

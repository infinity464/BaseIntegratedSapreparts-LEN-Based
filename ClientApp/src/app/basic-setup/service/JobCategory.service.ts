import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IJobCategoryPagination,JobCategoryPagination } from '../models/JobCategoryPagination'
import { JobCategory } from '../models/JobCategory';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class JobCategoryService {
  baseUrl = environment.apiUrl;
  JobCategorys: JobCategory[] = [];
  JobCategoryPagination = new JobCategoryPagination();
  constructor(private http: HttpClient) { }

  getJobCategorys(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IJobCategoryPagination>(this.baseUrl + '/job-category/get-jobCategories', { observe: 'response', params })
    .pipe(
      map(response => {
        this.JobCategorys = [...this.JobCategorys, ...response.body.items];
        this.JobCategoryPagination = response.body;
        return this.JobCategoryPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<JobCategory>(this.baseUrl + '/job-category/get-JobCategoryDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/job-category/update-JobCategory/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/job-category/save-JobCategory', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/job-category/delete-JobCategory/'+id);
  }

}

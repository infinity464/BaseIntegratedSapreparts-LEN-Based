import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IIssueTypePagination,IssueTypePagination } from '../models/IssueTypePagination'
import { IssueType } from '../models/IssueType';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class IssueTypeService {
  baseUrl = environment.apiUrl;
  IssueTypes: IssueType[] = [];
  IssueTypePagination = new IssueTypePagination();
  constructor(private http: HttpClient) { }

  getIssueTypes(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IIssueTypePagination>(this.baseUrl + '/issue-type/get-IssueTypes', { observe: 'response', params })
    .pipe(
      map(response => {
        this.IssueTypes = [...this.IssueTypes, ...response.body.items];
        this.IssueTypePagination = response.body;
        return this.IssueTypePagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<IssueType>(this.baseUrl + '/issue-type/get-IssueTypeDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/issue-type/update-IssueType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/issue-type/save-IssueType', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/issue-type/delete-IssueType/'+id);
  }

}

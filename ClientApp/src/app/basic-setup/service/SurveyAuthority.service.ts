import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {ISurveyAuthorityPagination,SurveyAuthorityPagination } from '../models/SurveyAuthorityPagination'
import { SurveyAuthority } from '../models/SurveyAuthority';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class SurveyAuthorityService {
  baseUrl = environment.apiUrl;
  SurveyAuthorities: SurveyAuthority[] = [];
  SurveyAuthorityPagination = new SurveyAuthorityPagination();
  constructor(private http: HttpClient) { }

  getSurveyAuthorities(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<ISurveyAuthorityPagination>(this.baseUrl + '/survey-authority/get-SurveyAuthorities', { observe: 'response', params })
    .pipe(
      map(response => {
        this.SurveyAuthorities = [...this.SurveyAuthorities, ...response.body.items];
        this.SurveyAuthorityPagination = response.body;
        return this.SurveyAuthorityPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<SurveyAuthority>(this.baseUrl + '/survey-authority/get-SurveyAuthorityDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/survey-authority/update-SurveyAuthority/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/survey-authority/save-SurveyAuthority', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/survey-authority/delete-SurveyAuthority/'+id);
  }

}

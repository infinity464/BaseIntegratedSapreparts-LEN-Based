import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {ISurveyItemTypePagination,SurveyItemTypePagination } from '../models/SurveyItemTypePagination'
import { SurveyItemType } from '../models/SurveyItemType';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class SurveyItemTypeService {
  baseUrl = environment.apiUrl;
  SurveyItemTypes: SurveyItemType[] = [];
  SurveyItemTypePagination = new SurveyItemTypePagination();
  constructor(private http: HttpClient) { }

  getSurveyItemTypes(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<ISurveyItemTypePagination>(this.baseUrl + '/survey-item-type/get-SurveyItemTypes', { observe: 'response', params })
    .pipe(
      map(response => {
        this.SurveyItemTypes = [...this.SurveyItemTypes, ...response.body.items];
        this.SurveyItemTypePagination = response.body;
        return this.SurveyItemTypePagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<SurveyItemType>(this.baseUrl + '/survey-item-type/get-SurveyItemTypeDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/survey-item-type/update-SurveyItemType/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/survey-item-type/save-SurveyItemType', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/survey-item-type/delete-SurveyItemType/'+id);
  }

}

import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IOpinionPagination,OpinionPagination } from '../models/OpinionPagination'
import { Opinion } from '../models/Opinion';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class OpinionService {
  baseUrl = environment.apiUrl;
  Opinions: Opinion[] = [];
  OpinionPagination = new OpinionPagination();
  constructor(private http: HttpClient) { }

  getOpinions(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IOpinionPagination>(this.baseUrl + '/opinion/get-Opinions', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Opinions = [...this.Opinions, ...response.body.items];
        this.OpinionPagination = response.body;
        return this.OpinionPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<Opinion>(this.baseUrl + '/opinion/get-OpinionDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/opinion/update-Opinion/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/opinion/save-Opinion', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/opinion/delete-Opinion/'+id);
  }

}

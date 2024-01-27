import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IProgresPagination,ProgresPagination } from '../models/ProgresPagination'
import { Progres } from '../models/Progres';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ProgresService {
  baseUrl = environment.apiUrl;
  Progress: Progres[] = [];
  ProgresPagination = new ProgresPagination();
  constructor(private http: HttpClient) { }

  getProgress(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IProgresPagination>(this.baseUrl + '/progres/get-Priorities', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Progress = [...this.Progress, ...response.body.items];
        this.ProgresPagination = response.body;
        return this.ProgresPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<Progres>(this.baseUrl + '/progres/get-ProgresDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/progres/update-Progres/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/progres/save-Progres', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/progres/delete-Progres/'+id);
  }

}

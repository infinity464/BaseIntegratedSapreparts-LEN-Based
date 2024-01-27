import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IByWhomPagination,ByWhomPagination } from '../models/ByWhomPagination'
import { ByWhom } from '../models/ByWhom';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ByWhomService {
  baseUrl = environment.apiUrl;
  ByWhoms: ByWhom[] = [];
  ByWhomPagination = new ByWhomPagination();
  constructor(private http: HttpClient) { }

  getByWhoms(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IByWhomPagination>(this.baseUrl + '/by-whom/get-ByWhoms', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ByWhoms = [...this.ByWhoms, ...response.body.items];
        this.ByWhomPagination = response.body;
        return this.ByWhomPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<ByWhom>(this.baseUrl + '/by-whom/get-ByWhomDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/by-whom/update-ByWhom/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/by-whom/save-ByWhom', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/by-whom/delete-ByWhom/'+id);
  }

}

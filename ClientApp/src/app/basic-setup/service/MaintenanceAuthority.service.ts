import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IMaintenanceAuthorityPagination,MaintenanceAuthorityPagination } from '../models/MaintenanceAuthorityPagination'
import { MaintenanceAuthority } from '../models/MaintenanceAuthority';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class MaintenanceAuthorityService {
  baseUrl = environment.apiUrl;
  MaintenanceAuthoritys: MaintenanceAuthority[] = [];
  MaintenanceAuthorityPagination = new MaintenanceAuthorityPagination();
  constructor(private http: HttpClient) { }

  getMaintenanceAuthoritys(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IMaintenanceAuthorityPagination>(this.baseUrl + '/maintenance-authority/get-MaintenanceAuthoritys', { observe: 'response', params })
    .pipe(
      map(response => {
        this.MaintenanceAuthoritys = [...this.MaintenanceAuthoritys, ...response.body.items];
        this.MaintenanceAuthorityPagination = response.body;
        return this.MaintenanceAuthorityPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<MaintenanceAuthority>(this.baseUrl + '/maintenance-authority/get-MaintenanceAuthorityDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/maintenance-authority/update-MaintenanceAuthority/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/maintenance-authority/save-MaintenanceAuthority', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/maintenance-authority/delete-MaintenanceAuthority/'+id);
  }

}

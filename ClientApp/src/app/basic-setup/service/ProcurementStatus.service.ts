import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IProcurementStatusPagination, ProcurementStatusPagination } from '../models/ProcurementStatusPagination'
import { ProcurementStatus } from '../models/ProcurementStatus';
import { map } from 'rxjs';
import {SelectedModel} from '../../../../src/app/core/models/selectedModel';

@Injectable({
  providedIn: 'root'
})
export class ProcurementStatusService {
  baseUrl = environment.apiUrl;
  ProcurementStatuss: ProcurementStatus[] = [];
  ProcurementStatusPagination = new ProcurementStatusPagination();
  constructor(private http: HttpClient) { }

  getProcurementStatuss(pageNumber, pageSize, searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    
    return this.http.get<IProcurementStatusPagination>(this.baseUrl + '/procurement-status/get-ProcurementStatuses', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ProcurementStatuss = [...this.ProcurementStatuss, ...response.body.items];
        this.ProcurementStatusPagination = response.body;
        return this.ProcurementStatusPagination;
      })
    );
   
  }

  

  find(id: number) {
    return this.http.get<ProcurementStatus>(this.baseUrl + '/procurement-status/get-ProcurementStatusDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/procurement-status/update-ProcurementStatus/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/procurement-status/save-ProcurementStatus', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/procurement-status/delete-ProcurementStatus/'+id);
  }

}

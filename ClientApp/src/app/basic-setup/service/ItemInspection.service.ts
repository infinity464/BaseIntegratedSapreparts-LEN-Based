import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IItemInspectionPagination,ItemInspectionPagination } from '../models/ItemInspectionPagination'
import { ItemInspection } from '../models/ItemInspection';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ItemInspectionService {
  baseUrl = environment.apiUrl;
  ItemInspections: ItemInspection[] = [];
  ItemInspectionPagination = new ItemInspectionPagination();
  constructor(private http: HttpClient) { }

  getItemInspections(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IItemInspectionPagination>(this.baseUrl + '/item-inspection/get-ItemInspections', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ItemInspections = [...this.ItemInspections, ...response.body.items];
        this.ItemInspectionPagination = response.body;
        return this.ItemInspectionPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<ItemInspection>(this.baseUrl + '/item-inspection/get-ItemInspectionDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/item-inspection/update-ItemInspection/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/item-inspection/save-ItemInspection', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/item-inspection/delete-ItemInspection/'+id);
  }

}

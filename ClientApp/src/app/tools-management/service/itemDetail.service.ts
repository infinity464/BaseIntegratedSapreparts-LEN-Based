import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IItemDetailPagination, ItemDetailPagination } from '../models/itemDetailPagination'
import { ItemDetail } from '../models/itemDetail';
import { map } from 'rxjs';
import { SelectedModel } from 'src/app/core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class ItemDetailService {
  baseUrl = environment.apiUrl;
  ItemDetails: ItemDetail[] = [];
  ItemDetailPagination = new ItemDetailPagination();
  constructor(private http: HttpClient) { }

  getItemDetails(pageNumber, pageSize, searchText) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    
    return this.http.get<IItemDetailPagination>(this.baseUrl + '/item-detail/get-ItemDetails', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ItemDetails = [...this.ItemDetails, ...response.body.items];
        this.ItemDetailPagination = response.body;
        return this.ItemDetailPagination;
      })
    );
   
  }

  getItemDetailsForTools(pageNumber, pageSize, searchText,sparesCategoryId) {

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('sparesCategoryId', sparesCategoryId.toString());
    //item-detail/get-itemDetailsForTools?PageSize=5&PageNumber=1&sparesCategoryId=2
    return this.http.get<IItemDetailPagination>(this.baseUrl + '/item-detail/get-itemDetailsForTools', { observe: 'response', params })
    .pipe(
      map(response => {
        this.ItemDetails = [...this.ItemDetails, ...response.body.items];
        this.ItemDetailPagination = response.body;
        return this.ItemDetailPagination;
      })
    );
   
  }
  getSelectedItemDetailListByDepartment(departmentNameId){
    return this.http.get<ItemDetail[]>(this.baseUrl + '/item-detail/get-selectedItemDetailListByDepartmentId?departmentNameId='+departmentNameId+'')
  }
  getselectedDepartmentNames(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/department-name/get-selectedDepartmentNames')
  }
  getselectedItemType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-type/get-selectedItemType')
  }
  getSelectedSchoolName(baseNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  }
  getselectedPresentStocks(departmentId){
    return this.http.get<any[]>(this.baseUrl + '/item-detail/get-presentStocks?departmentId='+departmentId)
  }
  getselectedTrades(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/trade/get-selectedTrades')
  }
  getSelectedItemCategory(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-category/get-selectedItemCategory')
  }
  getPartNoByDepartmentNameId(id:number){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-partnoByDepartmentNameId?departmentNameId=' + id);
  }
  getItemNameById(id:number){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/demand/get-itemNameByIdRequest?itemDetailId=' + id);
  }
  find(id: number) {
    return this.http.get<ItemDetail>(this.baseUrl + '/item-detail/get-ItemDetailDetail/' + id);
  }
  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/item-detail/update-ItemDetail/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/item-detail/save-ItemDetail', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/item-detail/delete-ItemDetail/'+id);
  }

}

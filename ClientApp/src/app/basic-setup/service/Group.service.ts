import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {IGroupPagination,GroupPagination } from '../models/GroupPagination'
import { Group } from '../models/Group';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class GroupService {
  baseUrl = environment.apiUrl;
  Groups: Group[] = [];
  GroupPagination = new GroupPagination();
  constructor(private http: HttpClient) { }

  getGroups(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<IGroupPagination>(this.baseUrl + '/group/get-Groups', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Groups = [...this.Groups, ...response.body.items];
        this.GroupPagination = response.body;
        return this.GroupPagination;
      })
    );
   
  }

  find(id: number) {
    return this.http.get<Group>(this.baseUrl + '/group/get-GroupDetail/' + id);
  }


  // getselecteddivision(){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/deno/get-selectedDeno')
  // }
   

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/group/update-Group/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/group/save-Group', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/group/delete-Group/'+id);
  }

}

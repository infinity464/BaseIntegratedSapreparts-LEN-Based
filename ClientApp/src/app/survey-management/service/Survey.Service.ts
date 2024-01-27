import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import {ISurveyPagination,SurveyPagination } from '../models/ISurveyPagination'
import { Survey } from '../models/Survey';
import { map } from 'rxjs';
import { SelectedModel } from '../../core/models/selectedModel';
@Injectable({
  providedIn: 'root'
})
export class SurveyService {
  baseUrl = environment.apiUrl;
  Surveys: Survey[] = [];
  SurveyPagination = new SurveyPagination();
  constructor(private http: HttpClient) { }

  getSurveys(pageNumber, pageSize, searchText) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    
    return this.http.get<ISurveyPagination>(this.baseUrl + '/survey-item/get-surveyItems', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Surveys = [...this.Surveys, ...response.body.items];
        this.SurveyPagination = response.body;
        return this.SurveyPagination;
      })
    );
   // 
  }

  //autocomplete for Course
  getSelectedCourseByName(courseName){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/Survey/get-autocompletePartNoByName?partNo='+courseName)
      .pipe(
        map((response:[]) => response.map(item => item))
      )
  }
   //autocomplete for By PartNo   
   getSelectedPartNoByDepartment(partNo){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-autocompletePartNoByDepartment?partNo='+partNo+'')
      .pipe(
        map((response:[]) => response.map(item => item))
      )
  }

  getSelectedSurveyAuthority(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/survey-authority/get-selectedSurveyAuthoritys')
  }
  getSelectedSurveyItemType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/survey-item-type/get-selectedSurveyItemTypes')
  }
  getSelectedEquipmentName(unitId,departmentId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/issue-register/get-selectedEquipmentNameForSurveyByParameterRequest?unitId='+unitId+'&departmentNameId='+departmentId);
  }
  // updateSurveyInformation(issueRegisterId,status){
  //  return this.http.get<any[]>(this.baseUrl + '/issue-register/get-issueRegisterListForSurvey?unitId='+unitId+'&departmentNameId='+departmentId);
  // }
  getSurveyList(unitId,departmentId){
    return this.http.get<any[]>(this.baseUrl + '/issue-register/get-issueRegisterListForSurvey?unitId='+unitId+'&departmentNameId='+departmentId);
  }
  getListForSurveyDeclare(unitId,departmentId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/issue-register/get-issueRegisterListForSurveyDeclareByParameterRequest?unitId='+unitId+'&departmentNameId='+departmentId)
  }
  getListForSurveyDeclareFilter(unitId,departmentId,itemCategoryId,itemDetailId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/issue-register/get-issueRegisterListForSurveyDeclareByParameterRequest?unitId='+unitId+'&departmentNameId='+departmentId+'&itemCategoryId='+itemCategoryId+'&itemDetailId='+itemDetailId+'')
  }
  // issue-register/get-issueRegisterListForSurveyDeclareByParameterRequest?unitId=13&departmentNameId=23&itemCategoryId=1&itemDetailId=4515
  updateSurvayStatus(issueRegisterId,status){
    return this.http.get<any>(this.baseUrl + '/issue-register/update-survayStatusOnIssueRegister?issueRegisterId='+issueRegisterId+'&status='+status)
  }

  getSelectedItemDetails(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-detail/get-selectedItemDetails')
  }
  getSelectedDeno(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/deno/get-selectedDeno')
  }
  getSelectedTrade(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/trade/get-selectedTrades')
  }
  getSelectedItemCategory(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-category/get-selectedItemCategory')
  }
  getSelectedManufacture(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/manufacture/get-selectedManufactures')
  }
  getSelectedFiscalYear(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/fiscal-year/get-selectedFiscalYear')
  }
  getSelectedItemType(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/item-type/get-selectedItemType')
  }
  getSelectedOccasionOfSurvey(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/occasion-of-Survey/get-selectedOccasionOfSurveys')
  }
  // getSelectedSurveyAuthority(){
  //   return this.http.get<SelectedModel[]>(this.baseUrl + '/Survey-authority/get-selectedSurveyAuthority')
  // }

  getSelectedDepartmentName(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/department-name/get-selectedDepartmentNames')
  }

  getSelectedConditionOfItem(){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/condition-of-item/get-selectedConditionOfItem')
  }
  getSelectedSchoolName(baseNameId){
    return this.http.get<SelectedModel[]>(this.baseUrl + '/base-School-name/get-selectedSchoolNames?thirdLevel='+baseNameId)
  }
  GetselectedSurveyById(SurveyId){
    return this.http.get<Survey[]>(this.baseUrl + '/Survey/get-selectedSurveyById?SurveyId='+SurveyId);
  }
  getSurveyListByDepartmentNameId(pageNumber, pageSize, searchText,sparesCategoryId,departmentId) { 

    let params = new HttpParams();

    params = params.append('searchText', searchText.toString());
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('sparesCategoryId', sparesCategoryId.toString());
    params = params.append('departmentNameId', departmentId.toString());
    
    return this.http.get<ISurveyPagination>(this.baseUrl + '/Survey/get-SurveyListByDepartmentNameId', { observe: 'response', params })
    .pipe(
      map(response => {
        this.Surveys = [...this.Surveys, ...response.body.items];
        this.SurveyPagination = response.body;
        return this.SurveyPagination;
      })
    );
   // 
  }

  find(id: number) {
    return this.http.get<Survey>(this.baseUrl + '/survey-item/get-surveyItemDetail/' + id);
  }

  update(id: number,model: any) {
    return this.http.put(this.baseUrl + '/survey-item/update-surveyItem/'+id, model);
  }
  submit(model: any) {
    return this.http.post(this.baseUrl + '/survey-item/save-surveyItem', model);
  } 
  saveSurvey(model: any) {
    return this.http.post(this.baseUrl + '/survey/save-Survey', model);
  } 
  delete(id:number){
    return this.http.delete(this.baseUrl + '/survey-item/delete-surveyItem/'+id);
  }

}

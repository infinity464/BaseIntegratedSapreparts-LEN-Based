import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
import {NewSurveyComponent} from '../survey-management/survey/new-survey/new-survey.component';
import {NewSurveyDeclareComponent} from '../survey-management/survey/survey-declare/new-surveydeclare.component'
//import { ItemDetailListComponent } from './itemdetail/itemdetail-list/itemdetail-list.component';
const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },

  // {
  //   path: 'add-survey',
  //   component: NewSurveyComponent,
  // },
  // {
  //   path: 'survey-declare',
  //   component: NewSurveyDeclareComponent,
  // },
  // { path: 'update-itemdetail/:itemDetailId', 
  // component: NewItemDetailComponent 
  // },
  // {
  //   path: 'add-itemdetail/:sparesCategoryId',
  //   component: NewItemDetailComponent,
  // },
  
  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class SurveyManagementRoutingModule { }

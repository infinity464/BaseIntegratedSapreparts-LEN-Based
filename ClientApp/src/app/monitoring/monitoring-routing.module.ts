import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';

import { FindItemDetailComponent } from './find-itemdetail/find-itemdetail.component';
import { FindDemandStateComponent } from './find-demandstate/find-demandstate.component';
import { FindDemandsComponent } from './find-demands/find-demands.component';
import { FindProcurementsComponent } from './find-procurements/find-procurements.component';
import { FindSftComponent } from './find-sft/find-sft.component';
import { FindIssuedItemComponent } from './find-issueditem/find-issueditem.component';
import { FindCorrectiveComponent } from './find-corrective/find-corrective.component';
import { FindAvailableQtyComponent } from './find-availableqty/find-availableqty.component';
import { FindPreventiveMaintenanceComponent } from './find-preventivemaintenance/find-preventivemaintenance.component';
import { FindScheduledMaintenanceComponent } from './find-scheduledmaintenance/find-scheduledmaintenance.component';
import { FindequipementNonOPLStateComponent } from './find-equipementnonoplstate/find-equipementnonoplstate.component';
import { FindItemMonitoringComponent } from './find-itemmonitoring/find-itemmonitoring.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },

  
  {
    path: 'find-itemdetail',
    component: FindItemDetailComponent,
  },
  
  {
    path: 'find-demandstate',
    component: FindDemandStateComponent,
  },

  {
    path: 'find-demands',
    component: FindDemandsComponent,
  },

  {
    path: 'find-procurements',
    component: FindProcurementsComponent,
  },

  {
    path: 'find-sft',
    component: FindSftComponent,
  },

  {
    path: 'find-issueditem',
    component: FindIssuedItemComponent,
  },

  {
    path: 'find-itemmoniotring',
    component: FindItemMonitoringComponent,
  },

  {
    path: 'find-corrective',
    component: FindCorrectiveComponent,
  },
  {
    path: 'find-equipementnonoplstate',
    component: FindequipementNonOPLStateComponent,
  },
  {
    path: 'find-availableqty',
    component: FindAvailableQtyComponent,
  },
  {
    path: 'find-preventivemaintenance',
    component: FindPreventiveMaintenanceComponent,
  },
  {
    path: 'find-scheduledmaintenance',
    component: FindScheduledMaintenanceComponent,
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

export class monitoringRoutingModule { }

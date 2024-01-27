import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
// import { MaintenanceTypeListComponent } from './maintenancetype/maintenancetype-list/maintenancetype-list.component';
// import { NewMaintenanceTypeComponent } from './maintenancetype/new-maintenancetype/new-maintenancetype.component';
import { MaintenancePlanningListComponent } from './maintenanceplanning/maintenanceplanning-list/maintenanceplanning-list.component';
import { NewMaintenancePlanningComponent } from './maintenanceplanning/new-maintenanceplanning/new-maintenanceplanning.component';
// import { MaintenanceCategoryListComponent } from '../maintenence-planning/maintenancecategory/maintenancecategory-list/maintenancecategory-list.component';
// import {NewMaintenanceCategoryComponent} from '../maintenence-planning/maintenancecategory/new-maintenancecategory/new-maintenancecategoryr.component'
import { TrainingCrewListComponent } from './trainingcrew/trainingcrew-list/trainingcrew-list.component';
import { NewTrainingCrewComponent } from './trainingcrew/new-trainingcrew/new-trainingcrew.component';
// import { MaintenanceSubCategoryListComponent } from '../maintenence-planning/maintenancesubcategory/maintenancesubcategory-list/maintenancesubcategory-list.component';
// import { NewMaintenanceSubCategoryComponent } from '../maintenence-planning/maintenancesubcategory/new-maintenancesubcategory/new-maintenancesubcategory.component';
import { NewMaintenanceScheduleComponent } from './maintenanceschedule/new-maintenanceschedule/new-maintenanceschedule.component';
import { MeaSquadronStateListComponent } from './measquadronstate/measquadronstate-list/measquadronstate-list.component';
import { NewMeaSquadronStateComponent } from './measquadronstate/new-measquadronstate/new-measquadronstate.component';
import { CallibrationStateListComponent } from './callibrationstate/callibrationstate-list/callibrationstate-list.component';
import { NewCallibrationStateComponent } from './callibrationstate/new-callibrationstate/new-callibrationstate.component';
import { NewPreventiveMaintenanceComponent } from './preventivemaintenance/new-preventivemaintenance/new-preventivemaintenance.component';
import {NewCorrectiveComponent} from './corrective/new-corrective/new-corrective.component';
//import {NewJobDescriptionScheduleMaintenenceComponent} from './jobdescriptionschedulemaintenence/new-jobdescriptionschedulemaintenence/new-jobdescriptionschedulemaintenence.component'
import {NewScheduleMaintenenceComponent} from './schedulemaintenence/new-schedulemaintenence/new-schedulemaintenence.component'
import {UpdateScheduleMaintenenceComponent} from './schedulemaintenence/update-schedulemaintenence/update-schedulemaintenence.component'
import {NewItemDetailComponent} from '../spares-management/itemdetail/new-itemdetail/new-itemdetail.component'
//import { NewPmsNumberEntryComponent } from '../basic-setup/pmsnumberentry/new-pmsnumberentry/new-pmsnumberentry.component';
import {NewMaintenanceCategoryComponent} from '../maintenence-planning/maintenancecategory/new-maintenancecategory/new-maintenancecategoryr.component'
import { SpareRequiredDetailsListComponent } from './sparerequireddetails-list/sparerequireddetails-list.component';
import {ScheduleMaintenenceListComponent} from './schedulemaintenence/schedulemaintenence-list/schedulemaintenence-list.component';
import {PreventiveMaintenanceListComponent} from './preventivemaintenance/preventivemaintenance-list/preventivemaintenance-list.component'

const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },
  {
     path: 'add-maintenancecategory',
    component:NewMaintenanceCategoryComponent ,
  },
  { 
    path: 'update-maintenancecategory/:maintenanceCategoryId', 
  component: NewMaintenanceCategoryComponent 
  },
  { 
    path: 'sparerequireddetails/:jobDescriptionEntryPmsId', 
    component: SpareRequiredDetailsListComponent 
  },
  {
    path: 'add-maintenancecategory',
    component: NewMaintenanceCategoryComponent,
  },

  // { path: 'update-pmsnumberentry/:pmsNumberEntryId', 
  // component: NewPmsNumberEntryComponent 
  // },
  {
    path: 'schedulemaintenence-list',
    component: ScheduleMaintenenceListComponent,
  },

  { path: 'update-itemdetail/:itemDetailId', 
  component: NewItemDetailComponent 
  },
  {
    path: 'add-itemdetail',
    component: NewItemDetailComponent,
  },
  {
    path: 'update-schedulemaintenence/:scheduleMaintenanceId',
    component: UpdateScheduleMaintenenceComponent,
  },
  // {
  //   path: 'add-jobdescriptionschedulemaintenence',
  //   component: NewJobDescriptionScheduleMaintenenceComponent,
  // },

  // {
  //   path: 'update-jobdescriptionschedulemaintenence/:scheduleMaintenanceId',
  //   component: NewJobDescriptionScheduleMaintenenceComponent,
  // },

  {
    path: 'add-schedulemaintenence',
    component: NewScheduleMaintenenceComponent,
  },

  {
    path: 'add-corrective',
    component: NewCorrectiveComponent,
  },
  {
    path: 'update-corrective/:correctiveMaintenenceId',
    component: NewCorrectiveComponent,
  },

  
  // {
  //   path: 'maintenancetype-list',
  //   component: MaintenanceTypeListComponent,
  // },
  // { path: 'update-maintenancetype/:maintenanceTypeId', 
  // component: NewMaintenanceTypeComponent 
  // },
  // {
  //   path: 'add-maintenancetype',
  //   component: NewMaintenanceTypeComponent,
  // },

  { 
    path: 'update-preventivemaintenance/:preventiveMaintenanceId', 
  component: NewPreventiveMaintenanceComponent 
  },
  {
    path: 'add-preventivemaintenance',
    component: NewPreventiveMaintenanceComponent,
  },
  {
    path: 'preventivemaintenance-list',
    component: PreventiveMaintenanceListComponent,
  },
  
  {
    path: 'measquadronstate-list',
    component: MeaSquadronStateListComponent,
  },
  { path: 'update-measquadronstate/:meaSquadronStateId', 
  component: NewMeaSquadronStateComponent 
  },
  {
    path: 'add-measquadronstate',
    component: NewMeaSquadronStateComponent,
  },
  {
    path: 'callibrationstate-list',
    component: CallibrationStateListComponent,
  },
  { path: 'update-callibrationstate/:callibrationStateId', 
  component: NewCallibrationStateComponent 
  },
  {
    path: 'add-callibrationstate',
    component: NewCallibrationStateComponent,
  },
  {
    path: 'add-maintenanceplanning',
    component: NewMaintenancePlanningComponent,
  },
  { path: 'update-maintenanceplanning/:maintenancePlanningId', 
  component: NewMaintenancePlanningComponent 
  },
  {
    path: 'add-maintenanceplanning',
    component: NewMaintenancePlanningComponent,
  },
  
  { path: 'update-maintenanceschedule/:maintenanceScheduleId', 
  component: NewMaintenanceScheduleComponent 
  },
  {
    path: 'add-maintenanceschedule',
    component: NewMaintenanceScheduleComponent,
  },

  //  {
  //   path: 'add-maintenancecategory',
  //   component:NewMaintenanceCategoryComponent ,
  // },
  // { 
  //   path: 'update-maintenancecategory/:maintenanceCategoryId', 
  // component: NewMaintenanceCategoryComponent 
  // },
  // {
  //   path: 'add-maintenancecategory',
  //   component: NewMaintenanceCategoryComponent,
  // },
  // {
  //   path: 'maintenancesubcategory-list',
  //   component: MaintenanceSubCategoryListComponent,
  // },
  // { path: 'update-maintenancesubcategory/:maintenanceSubCategoryId', 
  // component: NewMaintenanceSubCategoryComponent 
  // },
  // {
  //   path: 'add-maintenancesubcategory',
  //   component: NewMaintenanceSubCategoryComponent,
  // },
  {
    path: 'trainingcrew-list',
    component: TrainingCrewListComponent,
  },
  { path: 'update-trainingcrew/:trainingCrewId', 
  component: NewTrainingCrewComponent 
  },
  {
    path: 'add-trainingcrew',
    component: NewTrainingCrewComponent,
  },

  
  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class MaintenecePlanningRoutingModule { }

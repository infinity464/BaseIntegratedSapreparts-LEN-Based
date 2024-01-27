import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxDatatableModule } from '@swimlane/ngx-datatable';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MaintenecePlanningRoutingModule } from './maintenence-planning-routing.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MaterialFileInputModule } from 'ngx-material-file-input';
import { HttpClientModule } from '@angular/common/http';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
//import { NewPmsNumberEntryComponent } from '../basic-setup/pmsnumberentry/new-pmsnumberentry/new-pmsnumberentry.component';


// import { MaintenanceTypeListComponent } from './maintenancetype/maintenancetype-list/maintenancetype-list.component';
// import { NewMaintenanceTypeComponent } from './maintenancetype/new-maintenancetype/new-maintenancetype.component';
import { MaintenancePlanningListComponent } from './maintenanceplanning/maintenanceplanning-list/maintenanceplanning-list.component';
import { NewMaintenancePlanningComponent } from './maintenanceplanning/new-maintenanceplanning/new-maintenanceplanning.component';
// import { MaintenanceCategoryListComponent } from './maintenancecategory/maintenancecategory-list/maintenancecategory-list.component';
// import { NewMaintenanceCategoryComponent } from './maintenancecategory/new-maintenancecategory/new-maintenancecategoryr.component';
import { TrainingCrewListComponent } from './trainingcrew/trainingcrew-list/trainingcrew-list.component';
import { NewTrainingCrewComponent } from './trainingcrew/new-trainingcrew/new-trainingcrew.component';
// import { MaintenanceSubCategoryListComponent } from './maintenancesubcategory/maintenancesubcategory-list/maintenancesubcategory-list.component';
// import { NewMaintenanceSubCategoryComponent } from './maintenancesubcategory/new-maintenancesubcategory/new-maintenancesubcategory.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import {NewMaintenanceScheduleComponent } from './maintenanceschedule/new-maintenanceschedule/new-maintenanceschedule.component';
import { MeaSquadronStateListComponent } from './measquadronstate/measquadronstate-list/measquadronstate-list.component';
import { NewMeaSquadronStateComponent } from './measquadronstate/new-measquadronstate/new-measquadronstate.component';
import { CallibrationStateListComponent } from './callibrationstate/callibrationstate-list/callibrationstate-list.component';
import { NewCallibrationStateComponent } from './callibrationstate/new-callibrationstate/new-callibrationstate.component';
import { NewPreventiveMaintenanceComponent } from './preventivemaintenance/new-preventivemaintenance/new-preventivemaintenance.component';
import {NewCorrectiveComponent} from './corrective/new-corrective/new-corrective.component';
import {SpareStocklistComponent} from './corrective/spare-stocklist/spare-stocklist.component';
import { MatDialogModule } from '@angular/material/dialog';
import {ScheduleMaintenenceListComponent} from './schedulemaintenence/schedulemaintenence-list/schedulemaintenence-list.component';
import {NewScheduleMaintenenceComponent} from './schedulemaintenence/new-schedulemaintenence/new-schedulemaintenence.component'
import {UpdateScheduleMaintenenceComponent} from './schedulemaintenence/update-schedulemaintenence/update-schedulemaintenence.component'
import {NewItemDetailComponent} from '../spares-management/itemdetail/new-itemdetail/new-itemdetail.component'
import {NewMaintenanceCategoryComponent} from '../maintenence-planning/maintenancecategory/new-maintenancecategory/new-maintenancecategoryr.component'
import { SpareRequiredDetailsListComponent } from './sparerequireddetails-list/sparerequireddetails-list.component';
import {PreventiveMaintenanceListComponent} from './preventivemaintenance/preventivemaintenance-list/preventivemaintenance-list.component'

@NgModule({
  declarations: [    
   // MatDialogModule,
   NewScheduleMaintenenceComponent,
  // NewJobDescriptionScheduleMaintenenceComponent,
    SpareStocklistComponent,
    NewCorrectiveComponent,
    // MaintenanceTypeListComponent,
    // NewMaintenanceTypeComponent,
    MaintenancePlanningListComponent,
    NewMaintenancePlanningComponent,
    // MaintenanceCategoryListComponent,
    // NewMaintenanceCategoryComponent,
    TrainingCrewListComponent,
    NewTrainingCrewComponent,
    // MaintenanceSubCategoryListComponent,
    // NewMaintenanceSubCategoryComponent,
    NewMaintenanceScheduleComponent,
    MeaSquadronStateListComponent,
    NewMeaSquadronStateComponent,
    CallibrationStateListComponent,
    NewCallibrationStateComponent,
    NewPreventiveMaintenanceComponent,
    UpdateScheduleMaintenenceComponent,
    ScheduleMaintenenceListComponent,
    NewMaintenanceCategoryComponent,
    SpareRequiredDetailsListComponent,
    PreventiveMaintenanceListComponent

  ],
  imports: [
    MatDialogModule,
    CommonModule,
    MaintenecePlanningRoutingModule,
    CommonModule,
    FormsModule,  
    ReactiveFormsModule,
    NgxDatatableModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatStepperModule,
    MatSnackBarModule,
    MatButtonModule,
    MatIconModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MaterialFileInputModule,
    MatProgressSpinnerModule,
    HttpClientModule,
    MatAutocompleteModule,
    MatTooltipModule
    
  ]
})
export class MaintenecePlanningModule { }

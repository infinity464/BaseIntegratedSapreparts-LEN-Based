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
import { monitoringRoutingModule } from './monitoring-routing.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MaterialFileInputModule } from 'ngx-material-file-input';
import { HttpClientModule } from '@angular/common/http';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatTooltipModule } from '@angular/material/tooltip';
import {MatDialogModule} from '@angular/material/dialog';

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

// import { ProcurementListComponent } from './procurement/procurement-list/procurement-list.component';
@NgModule({
  declarations: [
    FindItemDetailComponent,
    FindDemandStateComponent,
    FindDemandsComponent,
    FindProcurementsComponent,
    FindSftComponent,
    FindIssuedItemComponent,
    FindCorrectiveComponent,
    FindAvailableQtyComponent,
    FindPreventiveMaintenanceComponent,
    FindScheduledMaintenanceComponent,
    FindequipementNonOPLStateComponent,
    FindItemMonitoringComponent
    // NewSurveyComponent,
    // NewSurveyDeclareComponent
    // ItemDetailListComponent,
    // NewItemDetailComponent,
  ],
  imports: [
    CommonModule,
    monitoringRoutingModule,
    // ToolsManagementRoutingModule,
    CommonModule,
    FormsModule,  
    ReactiveFormsModule,
    NgxDatatableModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
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
export class monitoringModule { }

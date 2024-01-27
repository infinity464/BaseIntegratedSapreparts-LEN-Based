import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { Page404Component } from '../authentication/page404/page404.component';
import { ItemDetailListComponent } from './itemdetail/itemdetail-list/itemdetail-list.component';
import { NewItemDetailComponent } from './itemdetail/new-itemdetail/new-itemdetail.component';
import { AcceptanceListComponent } from './acceptance/acceptance-list/acceptance-list.component';
import { NewAcceptanceComponent } from './acceptance/new-acceptance/new-acceptance.component';
import { DemandListComponent } from './demand/demand-list/demand-list.component';
import { NewDemandComponent } from './demand/new-demand/new-demand.component';
import{NewItemStorsComponent} from './store/new-store/new-itemstors.component'
import { ProcurementListComponent } from './procurement/procurement-list/procurement-list.component';
import { NewProcurementComponent } from './procurement/new-procurement/new-procurement.component';
//import { NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap/tooltip/tooltip.module';



const routes: Routes = [
  {
    path: '',
    redirectTo: 'signin',
    pathMatch: 'full'
  },

  {

    path: 'add-itemdetail',
    component: NewItemDetailComponent,
  },
  { path: 'update-itemdetail/:itemDetailId', 
  component: NewItemDetailComponent 
  },
  {
    path: 'add-itemdetail/:sparesCategoryId',
    component: NewItemDetailComponent,
  },

  {
    path: 'add-toolstore', 
    component: NewItemStorsComponent,
  },
  { path: 'update-itemstors/:itemStorId', 
  component: NewItemStorsComponent 
  },
  // {
  //   path: 'add-toolstore/:sparesCategoryId',
  //   component: NewItemStorsComponent,
  // },


  // {
  //   path: 'acceptance-list',
  //   component: AcceptanceListComponent,
  // },
  { path: 'update-acceptance/:acceptanceId', 
  component: NewAcceptanceComponent 
  },
  {
    path: 'add-acceptance',
    component: NewAcceptanceComponent,
  },
  // {
  //   path: 'procurement-list',
  //   component: ProcurementListComponent,
  // },
  { path: 'update-procurement/:procurementId', 
  component: NewProcurementComponent 
  },
  {
    path: 'add-procurement',
    component: NewProcurementComponent,
  },

  // {
  //   path: 'accounttype-list',
  //   component: AccountTypeListComponent,
  // },
  // { path: 'update-accounttype/:accountTypeId', 
  // component: NewAccountTypeComponent 
  // },
  // {
  //   path: 'add-accounttype',
  //   component: NewAccountTypeComponent,
  // },


  
  // {
  //   path: 'demand-list',
  //   component: DemandListComponent,
  // },
  { path: 'update-demand/:demandId', 
  component: NewDemandComponent, 
  },
  {
    path: 'add-demand',
    component: NewDemandComponent,
  },
  
  { path: '**', component: Page404Component },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})

export class ToolsManagementRoutingModule { }

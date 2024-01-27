// import { DashboardComponent as studentDashboard } from './../../student/dashboard/dashboard.component';
// import { DashboardComponent as teacherDashboard } from './../../teacher/dashboard/dashboard.component';
import { DashboardComponent as masterDashboard } from './../../master-dashboard/dashboard/dashboard.component';
import { DashboardComponent as employeeDashboard } from './../../employee/dashboard/dashboard.component';
import { DashboardComponent as userDashboard } from './../../user-dashboard/dashboard/dashboard.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main/main.component';
import { PendingDemandListComponent } from './pendingdemand-list/pendingdemand-list.component';
import { PendingProcurementListComponent } from './pendingprocurement-list/pendingprocurement-list.component';
import { PendingAcceptanceListComponent } from './pendingacceptance-list/pendingacceptance-list.component';
import { FlyingDetailsListComponent }from './flyingdetails-list/flyingdetails-list.component';
import { TrainingCrewListComponent }from './trainingcrew-list/trainingcrew-list.component';
import { DemandListComponent } from './demand-list/demand-list.component';
import { ACRunningHoursListComponent } from './acrunninghours-list/acrunninghours-list.component';
import { procurementListComponent } from './procurement-list/procurement-list.component';
import { acflyingprogramListComponent } from './acflyingprogram-list/acflyingprogram-list.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'main',
    pathMatch: 'full',
  },
  {
    path: 'main',
    component: MainComponent,
  },
  {
    path: 'admin-dashboard',
    component: employeeDashboard,
  },
  {
    path: 'user-dashboard',
    component: userDashboard,
  },
  {
    path: 'master-dashboard',
    component: masterDashboard,
  },
  {
    path: 'pendingDemand-list',
    component: PendingDemandListComponent,
  },
  {
    path: 'pendingProcurement-list',
    component: PendingProcurementListComponent,
  },
  {
    path: 'pendingAcceptance-list',
    component: PendingAcceptanceListComponent,
  },
  {
    path: 'demand-list',
    component: DemandListComponent,
  },
  {
    path: 'acrunninghours-list',
    component: ACRunningHoursListComponent,
  },
  {
    path: 'procurement-list',
    component: procurementListComponent,
  },
  {
    path: 'acflyingprogram-list',
    component: acflyingprogramListComponent,
  },
  {
    path: 'trainingcrew-list',
    component: TrainingCrewListComponent,
  },
  {
    path: 'flyingDetails-list/:departmentNameId/:airCraftNameId',
    component: FlyingDetailsListComponent,
  },
  // {
  //   path: 'teacher-dashboard',
  //   component: teacherDashboard,
  // },
  // {
  //   path: 'student-dashboard',
  //   component: studentDashboard,
  // },
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DashboardRoutingModule {}

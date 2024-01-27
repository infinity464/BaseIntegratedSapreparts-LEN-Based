import { Page404Component } from './authentication/page404/page404.component';
import { AuthLayoutComponent } from './layout/app-layout/auth-layout/auth-layout.component';
import { MainLayoutComponent } from './layout/app-layout/main-layout/main-layout.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './core/guard/auth.guard';
import { Role } from './core/models/role'; 
const routes: Routes = [
  {
    path: '',
    component: MainLayoutComponent,
    canActivate: [AuthGuard],
    children: [
      
      {
        path: 'basic-setup',
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin,Role.SuperAdmin,Role.User],
        },
        loadChildren: () =>
          import('./basic-setup/basic-setup.module').then((m) => m.BasicSetupModule),
      },

      {
        path: 'password',
        canActivate: [AuthGuard],
        data: {
          role: [Role.User,Role.SuperAdmin,Role.Admin],
        },
        loadChildren: () =>
          import('./password/password.module').then((m) => m.PasswordModule),
      },

      {
        path: 'spares-management',
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin,Role.SuperAdmin,Role.User],
        },
        loadChildren: () =>
          import('./spares-management/spares-management.module').then((m) => m.SparesManagementModule),
      },

      {
        path: 'tools-management',
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin,Role.SuperAdmin,Role.User],
        },
        loadChildren: () =>
          import('./tools-management/tools-management.module').then((m) => m.ToolsManagementModule),
      },
      {
        path: 'survey-management',
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin,Role.SuperAdmin,Role.User],
        },
        loadChildren: () =>
          import('./survey-management/survey-management.module').then((m) => m.SurveyManagementModule),
      },
      
      {
        path: 'monitoring',
        canActivate: [AuthGuard],
        data: {
          role: [Role.SuperAdmin,Role.Admin,Role.User],
        },
        loadChildren: () =>
          import('./monitoring/monitoring.module').then((m) => m.monitoringModule),
      },
      {
        path: 'issue-management',
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin,Role.SuperAdmin,Role.User],
        },
        loadChildren: () =>
          import('./issue-management/issue-management.module').then((m) => m.IssueManagementModule),
      },
      {
        path: 'maintenence-planning',
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin,Role.SuperAdmin,Role.User],
        },
        loadChildren: () =>
          import('./maintenence-planning/maintenence-planning.module').then((m) => m.MaintenecePlanningModule),
      },

      {
        path: 'security',
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin,Role.SuperAdmin,Role.User],
        },
        loadChildren: () =>
          import('./security/security.module').then((m) => m.SecurityModule),
      },
 
      {
        path: 'user-manual',
        canActivate: [AuthGuard],
        data: {
          role: [Role.SuperAdmin],
        },
        loadChildren: () =>
          import('./user-manual/user-manual.module').then((m) => m.UserManualModule),
      },

      { path: '', redirectTo: '/authentication/signin', pathMatch: 'full' },
      {
        path: 'admin',
        canActivate: [AuthGuard],
        data: {
          role: [Role.Admin,Role.SuperAdmin,Role.User],
        },
        loadChildren: () =>
          import('./admin/admin.module').then((m) => m.AdminModule),
      },
      

      // Extra components
      {
        path: 'extra-pages',
        loadChildren: () =>
          import('./extra-pages/extra-pages.module').then(
            (m) => m.ExtraPagesModule
          ),
      },
      
    ],
  },
  {
    path: 'authentication',
    component: AuthLayoutComponent,
    loadChildren: () =>
      import('./authentication/authentication.module').then(
        (m) => m.AuthenticationModule
      ),
  },
  { path: '**', component: Page404Component },
];
@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}

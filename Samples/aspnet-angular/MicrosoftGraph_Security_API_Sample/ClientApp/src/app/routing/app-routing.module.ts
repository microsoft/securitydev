import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MsalGuard } from '@azure/msal-angular';

// Pages
import { DashboardComponent } from '../components/pages/dashboard/dashboard.component';
import { AlertsComponent } from '../components/pages/alerts/alerts.component';
import { AlertDetailsComponent } from '../components/pages/alert-details/alert-details.component';
import { ActionsComponent } from '../components/pages/actions/actions.component';
import { SubscriptionsComponent } from '../components/pages/subscriptions/subscriptions.component';
import { SecureScoreComponent } from '../components/pages/secure-score/secure-score.component';
import { InvokeActionFormComponent } from '../components/pages/actions/invoke-action-form/invoke-action-form.component';


const routes: Routes = [
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [MsalGuard],
  },
  {
    path: 'alerts',
    component: AlertsComponent,
    canActivate: [MsalGuard]
  },
  {
    path: 'alerts/:id',
    component: AlertDetailsComponent,
    canActivate: [MsalGuard]
  },
  {
    path: 'actions/:id',
    component: ActionsComponent,
    canActivate: [MsalGuard]
  },
  {
    path: 'actions',
    component: ActionsComponent,
    canActivate: [MsalGuard]
  },
  {
    path: 'app-invoke-action-form',
    component: InvokeActionFormComponent,
    outlet: 'modal'
  },
  {
    path: 'subscriptions',
    component: SubscriptionsComponent,
    canActivate: [MsalGuard]
  },
  {
    path: 'secure-score',
    component: SecureScoreComponent,
    canActivate: [MsalGuard]
  },
  {
    path: '**',
    redirectTo: 'dashboard'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }

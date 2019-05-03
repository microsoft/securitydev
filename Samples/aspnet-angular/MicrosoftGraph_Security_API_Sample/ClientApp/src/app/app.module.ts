import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
// environment
import { environment } from 'src/environments/environment';
// ngx-clipboard
import { ClipboardModule } from 'ngx-clipboard';
// routing
import { AppRoutingModule } from './routing/app-routing.module';
// MSAL
import { MsalModule, MsalGuard, MsalService, MsalInterceptor } from '@azure/msal-angular';
// common components
import { CommonComponentsModule } from './components/common/common-components.module';
import { AppComponent } from './components/app.component';
// services
import { AlertValuesService, ActionValuesService } from './services';

// pages specific components
// actions page
import { ActionsPageModule } from './components/pages/actions/actions.module';
// alerts page
import { AlertsPageModule } from './components/pages/alerts/alerts.module';
// alerts page
import { AlertDetailsPageModule } from './components/pages/alert-details/alert-details.module';
// dashboard page
import { DashboardPageModule } from './components/pages/dashboard/dashboard.module';
// secure score page
import { SecureScorePageModule } from './components/pages/secure-score/secure-score.module';
// subscriptions page
import { SubscriptionsPageModule } from './components/pages/subscriptions/subscriptions.module';

@NgModule({
  declarations: [
    // common
    AppComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ClipboardModule,
    // auth module
    MsalModule.forRoot(environment.MSAL),
    // common reusable components
    CommonComponentsModule,
    // pages modules
    AlertsPageModule,
    AlertDetailsPageModule,
    SubscriptionsPageModule,
    ActionsPageModule,
    SecureScorePageModule,
    DashboardPageModule,
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: ModuleInitializer,
      deps: [AlertValuesService, ActionValuesService],
      multi: true
    },
    MsalGuard,
    MsalService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true
    },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

// Export function for initialize application
export function ModuleInitializer(alertValuesService: AlertValuesService, actionValuesService: ActionValuesService) {
  return () => Promise.all([
    alertValuesService.Initialize(),
    actionValuesService.Initialize()
  ]);
}

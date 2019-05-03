import { NgModule } from '@angular/core';
import { ChartsModule } from 'ng2-charts';
// ngx-perfect-scrollbar
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
    suppressScrollX: true
};
// common reusable components
import { CommonComponentsModule } from '../../common/common-components.module';
// page specific components
import { DashboardComponent } from './dashboard.component';
import { SeverityLevelsAlertsComponent } from './severity-levels-alerts/severity-levels-alerts.component';
import { DashboardListComponent } from './dashboard-list/dashboard-list.component';
import { DashboardItemComponent } from './dashboard-list/dashboard-item/dashboard-item.component';
import { SeverityLevelAlertsComponent } from './severity-levels-alerts/severity-level-alerts/severity-level-alerts.component';
import { DoughnutChartComponent } from './operation-statuses-alerts/operation-status-alerts/doughnut-chart/doughnut-chart.component';
import { OperationStatusAlertsComponent } from './operation-statuses-alerts/operation-status-alerts/operation-status-alerts.component';
import { OperationStatusesAlertsComponent } from './operation-statuses-alerts/operation-statuses-alerts.component';
// components
const components = [
    DashboardComponent,
    SeverityLevelsAlertsComponent,
    SeverityLevelAlertsComponent,
    DashboardListComponent,
    DashboardItemComponent,
    DoughnutChartComponent,
    OperationStatusAlertsComponent,
    OperationStatusesAlertsComponent,
];

@NgModule({
    imports: [
        CommonComponentsModule,
        PerfectScrollbarModule,
        FormsModule,
        ReactiveFormsModule,
        ChartsModule,
    ],
    providers: [
        {
            provide: PERFECT_SCROLLBAR_CONFIG,
            useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
        }
    ],
    declarations: components,
    exports: components
})

export class DashboardPageModule { }

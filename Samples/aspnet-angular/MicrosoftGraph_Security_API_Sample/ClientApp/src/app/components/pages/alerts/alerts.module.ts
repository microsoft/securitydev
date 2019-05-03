import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// modules
// ngx-perfect-scrollbar
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
    suppressScrollX: true
};
// common reusable components
import { CommonComponentsModule } from '../../common/common-components.module';
// components
import { AlertsComponent } from './alerts.component';
import { AlertListComponent } from './alert-list/alert-list.component';
import { AlertItemComponent } from './alert-list/alert-item/alert-item.component';
import { QueryListComponent } from './query-list/query-list.component';
import { QueryItemComponent } from './query-list/query-item/query-item.component';
import { FiltersComponent } from './filters/filters.component';
import { DisplayNamePipe } from './alert-list/display-name-pipe/display-name.pipe';

const components = [
    AlertsComponent,
    AlertListComponent,
    AlertItemComponent,
    QueryListComponent,
    QueryItemComponent,
    FiltersComponent,
    DisplayNamePipe
];

@NgModule({
    imports: [
        CommonComponentsModule,
        PerfectScrollbarModule,
        FormsModule,
        ReactiveFormsModule,
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

export class AlertsPageModule { }

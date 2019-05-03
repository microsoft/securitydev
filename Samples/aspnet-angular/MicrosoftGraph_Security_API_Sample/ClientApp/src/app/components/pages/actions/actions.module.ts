import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// ngx-perfect-scrollbar
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
    suppressScrollX: true
};
// common reusable components
import { CommonComponentsModule } from '../../common/common-components.module';
// page specific components
import { ActionsComponent } from './actions.component';
import { ActionListComponent } from './action-list/action-list.component';
import { ActionItemComponent } from './action-list/action-item/action-item.component';
import { FiltersComponent } from './filters/filters.component';
import { InvokeActionFormComponent } from './invoke-action-form/invoke-action-form.component';

// components
const components = [
    ActionsComponent,
    ActionListComponent,
    ActionItemComponent,
    FiltersComponent,
    InvokeActionFormComponent
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

export class ActionsPageModule { }

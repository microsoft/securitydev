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
import { SubscriptionListComponent } from './subscription-list/subscription-list.component';
import { SubscriptionItemComponent } from './subscription-list/subscription-item/subscription-item.component';
import { SubscriptionsComponent } from './subscriptions.component';
import { FiltersComponent } from './filters/filters.component';
import { SubscriptionQueryListComponent } from './query-list/query-list.component';
import { SubscriptionQueryItemComponent } from './query-list/query-item/query-item.component';

// components
const components = [
    SubscriptionsComponent,
    SubscriptionListComponent,
    FiltersComponent,
    SubscriptionItemComponent,
    SubscriptionQueryListComponent,
    SubscriptionQueryItemComponent
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

export class SubscriptionsPageModule { }

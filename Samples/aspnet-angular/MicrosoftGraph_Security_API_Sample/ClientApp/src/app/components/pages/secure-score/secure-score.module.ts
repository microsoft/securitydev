import { NgModule } from '@angular/core';
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
import { SecureScoreComponent } from './secure-score.component';
import { SecureScoreExpandableListComponent } from './components/secure-score-expandable-list/secure-score-expandable-list.component';
// services


const components = [
    SecureScoreExpandableListComponent,
    SecureScoreComponent,
];

@NgModule({
    imports: [
        CommonComponentsModule,
        PerfectScrollbarModule,
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

export class SecureScorePageModule { }

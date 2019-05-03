import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from 'src/app/routing/app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OwlDateTimeModule, OWL_DATE_TIME_FORMATS } from 'ng-pick-datetime';
import { OwlMomentDateTimeModule } from 'ng-pick-datetime-moment';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// charts modules
import { ChartsModule } from 'ng2-charts';
import { GoogleChartsModule } from 'angular-google-charts';
// ngx-perfect-scrollbar
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
    suppressScrollX: true
};
// components
import { ButtonComponent } from './buttons';
import { ContentHeaderComponent } from './content-header/content-header.component';
import { BreadcrumbsComponent } from './content-header/breadcrumbs/breadcrumbs.component';
import { HeaderComponent } from './header/header.component';
import { UserComponent } from './header/user/user.component';
import { HeaderMenuComponent } from './header/header-menu/header-menu.component';
import { UserPanelComponent } from './user-panel/user-panel.component';
import { TextfieldComponent } from './inputs/textfield/textfield.component';
import { TextareaComponent } from './inputs/textarea/textarea.component';
import { CheckboxGroupItemComponent } from './inputs/checkbox-group/checkbox-group-item/checkbox-group-item.component';
import { CheckboxGroupComponent } from './inputs/checkbox-group/checkbox-group.component';
import { CheckboxComponent } from './inputs/checkbox/checkbox.component';
import { RadioComponent } from './inputs/radio/radio.component';
import { MainSidebarComponent } from './main-sidebar/main-sidebar.component';
import { SelectComponent } from './inputs/select/select.component';
import { SeverityLevelComponent } from './severity-level/severity-level.component';
import { DateTimePickerComponent } from './inputs/datetime/datetime-picker.component';
import { ExpandableListComponent } from './expandable-list/expandable-list.component';
import { UserLogoComponent } from './user-logo/user-logo.component';
import { BarDiagramComponent } from './secure-score-chart/bar-diagram/bar-diagram.component';
import { SecureScoreChartComponent } from './secure-score-chart/secure-score-chart.component';
import { DoughnutDiagramComponent } from './secure-score-chart/doughnut-diagram/doughnut-diagram.component';
import { InvokeActionFormComponent } from './invoke-action-form/invoke-action-form.component';
import { ModalContainerComponent } from './modal-container/modal-container.component';
import { LoaderComponent } from './loader/loader.component';
import { SafeDecodeHtmlPipe } from './safe-decode-html-pipe/safe-decode-html.pipe';

export const MOMENT_FORMATS = {
    parseInput: 'l LT',
    fullPickerInput: 'l LT',
    datePickerInput: 'l',
    timePickerInput: 'LT',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
};

const components = [
    ButtonComponent,
    ContentHeaderComponent,
    BreadcrumbsComponent,
    HeaderComponent,
    UserComponent,
    TextfieldComponent,
    TextareaComponent,
    MainSidebarComponent,
    SelectComponent,
    CheckboxGroupItemComponent,
    CheckboxGroupComponent,
    CheckboxComponent,
    RadioComponent,
    SeverityLevelComponent,
    DateTimePickerComponent,
    ExpandableListComponent,
    HeaderMenuComponent,
    UserPanelComponent,
    UserLogoComponent,
    BarDiagramComponent,
    SecureScoreChartComponent,
    DoughnutDiagramComponent,
    InvokeActionFormComponent,
    ModalContainerComponent,
    LoaderComponent,
    SafeDecodeHtmlPipe,
];

@NgModule({
    imports: [
        CommonModule,
        AppRoutingModule,
        FormsModule,
        ReactiveFormsModule,
        OwlDateTimeModule,
        OwlMomentDateTimeModule,
        BrowserAnimationsModule,
        PerfectScrollbarModule,
        ChartsModule,
        GoogleChartsModule.forRoot(),
    ],
    providers: [
        { provide: OWL_DATE_TIME_FORMATS, useValue: MOMENT_FORMATS },
        { provide: PERFECT_SCROLLBAR_CONFIG, useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG }
    ],
    declarations: components,
    exports: components
})

export class CommonComponentsModule { }

import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
// modules
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
// ngx-perfect-scrollbar
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
    suppressScrollX: true
};
// time-ago pipe
import { TimeAgoPipe } from 'time-ago-pipe';
// common reusable components
import { CommonComponentsModule } from '../../common/common-components.module';
//  main panel components
import { ClickablePropertyComponent } from './common/clickable-property/clickable-property.component';
import { UserClickablePropertyComponent } from './common/user-clickable-property/user-clickable-property.component';
import { HostClickablePropertyComponent } from './common/host-clickable-property/host-clickable-property.component';
import { BooleanClickablePropertyComponent } from './common/boolean-clickable-property/boolean-clickable-property.component';
import { DatetimeClickablePropertyComponent } from './common/datetime-clickable-property/datetime-clickable-property.component';
import { AlertDetailsComponent } from './alert-details.component';
import { AlertDetailsHeaderComponent } from './header/header.component';
import { AlertDetailsMainInfoComponent } from './main-info/main-info.component';
import { BooleanPipe } from './common/boolean-pipe/boolean.pipe';
// forms
import { UpdateAlertFormComponent } from './update-alert-form/update-alert-form.component';
// alert details components
import { StringListComponent } from './details-items/string-list/string-list.component';
import { ActivityHistoryComponent } from './details-items/activity-history/activity-history.component';
import { ActivityHistoryItemComponent } from './details-items/activity-history/activity-history-item/activity-history-item.component';
import { UserListComponent } from './details-items/user-list/user-list.component';
import { UserItemComponent } from './details-items/user-list/user-item/user-item.component';
import { FileListComponent } from './details-items/file-list/file-list.component';
import { FileListItemComponent } from './details-items/file-list/file-list-item/file-list-item.component';
import { HostListComponent } from './details-items/host-list/host-list.component';
import { HostItemComponent } from './details-items/host-list/host-item/host-item.component';
import { NetworkConnectionListComponent } from './details-items/network-connection-list/network-connection-list.component';
// tslint:disable-next-line:max-line-length
import { NetworkConnectionItemComponent } from './details-items/network-connection-list/network-connection-item/network-connection-item.component';
import { RadioClickablePropertyComponent } from './common/radio-clickable-property/radio-clickable-property.component';
import { TriggerListComponent } from './details-items/trigger-list/trigger-list.component';
import { TriggerListItemComponent } from './details-items/trigger-list/trigger-item/trigger-item.component';
import { ProcessListComponent } from './details-items/process-list/process-list.component';
import { ProcessItemComponent } from './details-items/process-list/process-item/process-item.component';
import { RegistryKeyUpdateListComponent } from './details-items/registry-key-update-list/registry-key-update-list.component';
// tslint:disable-next-line:max-line-length
import { RegistryKeyUpdateItemComponent } from './details-items/registry-key-update-list/registry-key-update-item/registry-key-update-item.component';
import { MalwareStateItemComponent } from './details-items/malware-state-list/malware-state-item/malware-state-item.component';
import { MalwareStateListComponent } from './details-items/malware-state-list/malware-state-list.component';
// tslint:disable-next-line:max-line-length
import { VulnerabilityStateItemComponent } from './details-items/vulnerability-state-list/vulnerability-state-item/vulnerability-state-item.component';
import { VulnerabilityStateListComponent } from './details-items/vulnerability-state-list/vulnerability-state-list.component';
import { CloudAppStateListComponent } from './details-items/cloud-app-state-list/cloud-app-state-list.component';
import { CloudAppStateItemComponent } from './details-items/cloud-app-state-list/cloud-app-state-item/cloud-app-state-item.component';
import { UrlListComponent } from './details-items/url-list/url-list.component';

const components = [
    AlertDetailsComponent,
    BooleanPipe,
    AlertDetailsHeaderComponent,
    AlertDetailsMainInfoComponent,
    ClickablePropertyComponent,
    BooleanClickablePropertyComponent,
    UserClickablePropertyComponent,
    HostClickablePropertyComponent,
    RadioClickablePropertyComponent,
    DatetimeClickablePropertyComponent,
    UpdateAlertFormComponent,
    StringListComponent,
    ActivityHistoryComponent,
    ActivityHistoryItemComponent,
    TimeAgoPipe,
    UserListComponent,
    UserItemComponent,
    HostListComponent,
    HostItemComponent,
    FileListComponent,
    FileListItemComponent,
    NetworkConnectionListComponent,
    NetworkConnectionItemComponent,
    TriggerListComponent,
    TriggerListItemComponent,
    ProcessListComponent,
    ProcessItemComponent,
    RegistryKeyUpdateListComponent,
    RegistryKeyUpdateItemComponent,
    MalwareStateListComponent,
    MalwareStateItemComponent,
    VulnerabilityStateListComponent,
    VulnerabilityStateItemComponent,
    CloudAppStateListComponent,
    CloudAppStateItemComponent,
    UrlListComponent
];

@NgModule({
    imports: [
        CommonComponentsModule,
        PerfectScrollbarModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule
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

export class AlertDetailsPageModule { }

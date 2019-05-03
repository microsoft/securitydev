import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
// models
import { AlertDetails, HostSecurityState, User } from 'src/app/models/graph';

@Component({
    selector: 'app-alert-details-main-info',
    templateUrl: './main-info.component.html',
    styleUrls: ['./main-info.component.css']
})
export class AlertDetailsMainInfoComponent implements OnChanges {
    @Input() alertDetails: AlertDetails;
    @Output() valueClick: EventEmitter<{ key: string, value: string }>;

    public user: User;
    public host: HostSecurityState;

    constructor() {
        this.valueClick = new EventEmitter<{ key: string, value: string }>();
    }

    ngOnChanges(changes: SimpleChanges): void {
        this.user = this.alertDetails.users ? this.alertDetails.userAccountDevices[0] : null;
        this.host = this.alertDetails.hosts ? this.alertDetails.hosts[0] : null;
    }

    public onValueClick(filterValue: { key: string, value: string }): void {
        if (filterValue.value) {
            this.valueClick.emit(filterValue);
        }
    }
}

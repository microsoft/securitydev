import { Component, Input, Output, EventEmitter } from '@angular/core';
// models
import { Action } from 'src/app/models/graph/action.model';

const filterKeys = {
    destinationaddress: 'netconn:destinationaddress',
    destinationdomain: 'netconn:destinationdomain',
    destinationurl: 'netconn:destinationurl',
    destinationport: 'netconn:destinationport'
};

@Component({
    selector: 'app-action-item',
    templateUrl: './action-item.component.html',
    styleUrls: ['./action-item.component.css']
})
export class ActionItemComponent {
    @Input() public item: Action;
    @Output() public filterValueClick: EventEmitter<{ key: string, value: string }>;

    constructor() {
        this.filterValueClick = new EventEmitter<{ key: string, value: string }>();
    }

    public onTargetValueClick(): void {
        if (this.item && this.item.target && this.item.target.name && this.item.target.value) {
          const name = this.item.target.name;
          const value = this.item.target.value.toLowerCase();
            const key = filterKeys[name.toLowerCase()];
            if (key && value) {
                this.filterValueClick.emit({ key, value });
            }
        }
    }

    public onAlertIdClick(): void {
        if (this.item && this.item.reason && this.item.reason.alertId) {
            const alertId = this.item.reason.alertId;
            const key = 'alert:id';
            this.filterValueClick.emit({ key, value: alertId });
        }
    }
}

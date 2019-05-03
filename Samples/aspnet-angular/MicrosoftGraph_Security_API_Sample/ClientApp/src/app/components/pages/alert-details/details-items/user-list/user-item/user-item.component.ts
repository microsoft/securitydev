import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
// models
import { User, Device } from 'src/app/models/graph';

@Component({
    selector: 'app-user-item',
    templateUrl: './user-item.component.html',
    styleUrls: ['./user-item.component.css']
})
export class UserItemComponent implements OnChanges {
    @Input() user: User;
    @Output() valueClick: EventEmitter<{ key: string, value: string }>;
    public selectedDevice: Device;

    constructor() {
        this.valueClick = new EventEmitter<{ key: string, value: string }>();
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (this.user && this.user.registeredDevices) {
            this.selectedDevice = this.user.registeredDevices[0];
        }
    }

    public onValueClick(filterValue: { key: string, value: string }): void {
        if (filterValue.value) {
            this.valueClick.emit(filterValue);
        }
    }
}

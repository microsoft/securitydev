import { Component, Input, Output, EventEmitter } from '@angular/core';
// models
import { HostSecurityState } from 'src/app/models/graph';

@Component({
    selector: 'app-host-list',
    templateUrl: './host-list.component.html',
    styleUrls: ['./host-list.component.css']
})
export class HostListComponent {
    public title = 'Hosts';
    @Input() isExpanded = true;
    @Input() hosts: HostSecurityState[];
    @Output() valueClick: EventEmitter<{ key: string, value: string }>;

    constructor() {
        this.valueClick = new EventEmitter<{ key: string, value: string }>();
    }

    public onValueClick(filterValue: { key: string, value: string }): void {
        if (filterValue.value) {
            this.valueClick.emit(filterValue);
        }
    }
}

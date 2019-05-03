import { Component, Input, Output, EventEmitter } from '@angular/core';
// models
import { HostSecurityState } from 'src/app/models/graph';

@Component({
    selector: 'app-host-item',
    templateUrl: './host-item.component.html',
    styleUrls: ['./host-item.component.css']
})
export class HostItemComponent {
    @Input() host: HostSecurityState;
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

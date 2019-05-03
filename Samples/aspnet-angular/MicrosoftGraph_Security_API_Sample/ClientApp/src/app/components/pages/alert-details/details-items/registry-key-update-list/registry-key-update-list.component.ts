import { Component, Input, Output, EventEmitter } from '@angular/core';
// models
import { RegistryKeyState } from 'src/app/models/graph';

@Component({
    selector: 'app-registry-key-update-list',
    templateUrl: './registry-key-update-list.component.html',
    styleUrls: ['./registry-key-update-list.component.css']
})
export class RegistryKeyUpdateListComponent {
    public title = 'Registry Key Updates';
    @Input() isExpanded = true;
    @Input() items: RegistryKeyState[];
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

import { Component, Input, Output, EventEmitter } from '@angular/core';
// models
import { RegistryKeyState } from 'src/app/models/graph';

@Component({
    selector: 'app-registry-key-update-item',
    templateUrl: './registry-key-update-item.component.html',
    styleUrls: ['./registry-key-update-item.component.css']
})
export class RegistryKeyUpdateItemComponent {
    @Input() item: RegistryKeyState;
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

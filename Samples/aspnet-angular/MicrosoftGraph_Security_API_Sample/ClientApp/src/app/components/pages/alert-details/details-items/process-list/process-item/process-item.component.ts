import { Component, Input, Output, EventEmitter } from '@angular/core';
// models
import { Process } from 'src/app/models/graph';

@Component({
    selector: 'app-process-item',
    templateUrl: './process-item.component.html',
    styleUrls: ['./process-item.component.css']
})
export class ProcessItemComponent {
    @Input() item: Process;
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

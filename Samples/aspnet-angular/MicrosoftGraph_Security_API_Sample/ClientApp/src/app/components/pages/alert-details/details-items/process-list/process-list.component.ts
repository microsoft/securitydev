import { Component, Input, Output, EventEmitter } from '@angular/core';
// models
import { Process } from 'src/app/models/graph';

@Component({
    selector: 'app-process-list',
    templateUrl: './process-list.component.html',
    styleUrls: ['./process-list.component.css']
})
export class ProcessListComponent {
    public title = 'Processes';
    @Input() isExpanded = true;
    @Input() items: Process[];
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

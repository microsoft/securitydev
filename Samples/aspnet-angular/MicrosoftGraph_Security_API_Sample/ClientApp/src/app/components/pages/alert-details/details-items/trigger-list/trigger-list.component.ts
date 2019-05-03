import { Component, Input, Output, EventEmitter } from '@angular/core';
// models
import { Trigger } from 'src/app/models/graph';

@Component({
    selector: 'app-trigger-list',
    templateUrl: './trigger-list.component.html',
    styleUrls: ['./trigger-list.component.css']
})
export class TriggerListComponent {
    public title = 'Triggers';
    @Input() isExpanded = true;
    @Input() items: Trigger[];
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

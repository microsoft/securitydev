import { Component, Input, Output, EventEmitter } from '@angular/core';
// models
import { Trigger } from 'src/app/models/graph';

@Component({
    selector: 'app-trigger-item',
    templateUrl: './trigger-item.component.html',
    styleUrls: ['./trigger-item.component.css']
})
export class TriggerListItemComponent {
    @Input() public item: Trigger;
    @Output() public valueClick: EventEmitter<{ key: string, value: string }>;

    constructor() {
        this.valueClick = new EventEmitter<{ key: string, value: string }>();
    }

    public onValueClick(key: string, value: string): void {
        if (value) {
            this.valueClick.emit({ key, value });
        }
    }
}

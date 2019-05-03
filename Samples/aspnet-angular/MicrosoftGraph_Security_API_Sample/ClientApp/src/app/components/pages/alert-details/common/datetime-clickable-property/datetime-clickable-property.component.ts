import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'app-datetime-clickable-property',
    templateUrl: './datetime-clickable-property.component.html',
    styleUrls: ['./datetime-clickable-property.component.css']
})
export class DatetimeClickablePropertyComponent {
    @Input() title: string;
    @Input() value: string;
    @Input() filterKey: string;
    @Input() clickable = true;
    @Output() valueClick: EventEmitter<{ key: string, value: string }>;

    constructor() {
        this.valueClick = new EventEmitter<{ key: string, value: string }>();
    }

    onClick() {
        if (this.clickable && this.value && this.filterKey) {
            this.valueClick.emit({
                key: this.filterKey,
                value: this.value
            });
        }
    }
}

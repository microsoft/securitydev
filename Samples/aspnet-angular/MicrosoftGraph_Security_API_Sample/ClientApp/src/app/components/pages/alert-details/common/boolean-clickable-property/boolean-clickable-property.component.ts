import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'app-boolean-clickable-property',
    templateUrl: './boolean-clickable-property.component.html',
    styleUrls: ['./boolean-clickable-property.component.css']
})
export class BooleanClickablePropertyComponent {
    @Input() title: string;
    @Input() value: boolean;
    @Input() filterKey: string;
    @Input() clickable = true;
    @Output() valueClick: EventEmitter<{ key: string, value: string }>;

    constructor() {
        this.valueClick = new EventEmitter<{ key: string, value: string }>();
    }

    get isBoolean(): boolean {
        return typeof this.value === 'boolean';
    }

    onClick() {
        if (this.clickable && this.isBoolean && this.filterKey) {
            this.valueClick.emit({
                key: this.filterKey,
                value: this.value.toString()
            });
        }
    }
}

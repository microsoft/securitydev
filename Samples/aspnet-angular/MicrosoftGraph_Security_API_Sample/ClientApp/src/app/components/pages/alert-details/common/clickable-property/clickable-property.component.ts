import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'app-clickable-property',
    templateUrl: './clickable-property.component.html',
    styleUrls: ['./clickable-property.component.css']
})
export class ClickablePropertyComponent {
    @Input() title: string;
    @Input() value: string;
    @Input() filterKey: string;
    @Input() filterValue: string;
    @Input() cropInlineText = true;
    @Input() clickable = true;
    @Output() valueClick: EventEmitter<{ key: string, value: string }>;

    constructor() {
        this.valueClick = new EventEmitter<{ key: string, value: string }>();
    }

    onClick() {
        if (this.clickable && this.value && this.filterKey) {
            this.valueClick.emit({
                key: this.filterKey,
                value: this.filterValue || this.value
            });
        }
    }
}

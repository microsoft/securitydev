import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'app-alert-string-list',
    templateUrl: './string-list.component.html',
    styleUrls: ['./string-list.component.css']
})
export class StringListComponent {
    @Input() title: string;
    @Input() isExpanded = true;
    @Input() items: string[];
    @Input() filterKey: string;
    @Input() clickable = true;
    @Output() valueClick: EventEmitter<{ key: string, value: string }>;

    constructor() {
        this.valueClick = new EventEmitter<{ key: string, value: string }>();
    }

    public onValueClick(value: string): void {
        if (this.clickable && this.filterKey && value) {
            this.valueClick.emit({
                key: this.filterKey,
                value
            });
        }
    }
}

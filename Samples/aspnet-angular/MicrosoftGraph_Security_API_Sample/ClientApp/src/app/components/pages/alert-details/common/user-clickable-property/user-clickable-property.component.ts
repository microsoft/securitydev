import { Component, Input, Output, EventEmitter } from '@angular/core';
// models
import { User } from 'src/app/models/graph';

@Component({
    selector: 'app-user-clickable-property',
    templateUrl: './user-clickable-property.component.html',
    styleUrls: ['./user-clickable-property.component.css']
})
export class UserClickablePropertyComponent {
    @Input() title: string;
    @Input() user: User;
    @Input() filterKey: string;
    @Input() filterValue: string;
    @Input() clickable = true;
    @Output() valueClick: EventEmitter<{ key: string, value: string }>;

    constructor() {
        this.valueClick = new EventEmitter<{ key: string, value: string }>();
    }

    getDisplayValue(): string {
        return this.user ? (this.user.displayName || this.user.upn) : '';
    }

    onClick() {
        if (this.clickable && this.user && this.filterKey) {
            this.valueClick.emit({
                key: this.filterKey,
                // by default filter by user upn
                value: this.filterValue || this.user.upn
            });
        }
    }
}

import { Component, Input, Output, EventEmitter } from '@angular/core';
// models
import { HostSecurityState } from 'src/app/models/graph';

@Component({
    selector: 'app-host-clickable-property',
    templateUrl: './host-clickable-property.component.html',
    styleUrls: ['./host-clickable-property.component.css']
})
export class HostClickablePropertyComponent {
    @Input() title: string;
    @Input() host: HostSecurityState;
    @Input() filterKey: string;
    @Input() filterValue: string;
    @Input() clickable = true;
    @Output() valueClick: EventEmitter<{ key: string, value: string }>;

    constructor() {
        this.valueClick = new EventEmitter<{ key: string, value: string }>();
    }

    getDisplayValue(): string {
        return this.host ? this.host.fqdn : '';
    }

    onClick() {
        if (this.clickable && this.host && this.filterKey) {
            this.valueClick.emit({
                key: this.filterKey,
                // by default filter by this.host.fqdn
                value: this.filterValue || this.host.fqdn
            });
        }
    }
}

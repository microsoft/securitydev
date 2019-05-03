import { Component, Input, Output, EventEmitter } from '@angular/core';
// models
import { CloudAppSecurityState } from 'src/app/models/graph';

@Component({
    selector: 'app-cloud-app-state-list',
    templateUrl: './cloud-app-state-list.component.html',
    styleUrls: ['./cloud-app-state-list.component.css']
})
export class CloudAppStateListComponent {
    public title = 'Cloud Application States';
    @Input() isExpanded = true;
    @Input() items: CloudAppSecurityState[];
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

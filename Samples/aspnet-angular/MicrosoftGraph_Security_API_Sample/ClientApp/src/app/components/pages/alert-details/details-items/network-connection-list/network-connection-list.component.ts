import { Component, Input, Output, EventEmitter } from '@angular/core';
// models
import { NetworkConnection } from 'src/app/models/graph';

@Component({
    selector: 'app-network-connection-list',
    templateUrl: './network-connection-list.component.html',
    styleUrls: ['./network-connection-list.component.css']
})
export class NetworkConnectionListComponent {
    public title = 'Network connections';
    @Input() isExpanded = true;
    @Input() connections: NetworkConnection[];
    @Output() valueClick: EventEmitter<{ key: string, value: string }>;
    @Output() addAction: EventEmitter<{ name: string, value: string }>;

    constructor() {
        this.valueClick = new EventEmitter<{ key: string, value: string }>();
        this.addAction = new EventEmitter<{ name: string, value: string }>();
    }

    public onValueClick(filterValue: { key: string, value: string }): void {
        if (filterValue.value) {
            this.valueClick.emit(filterValue);
        }
    }

    public addToAction(actionValue: { name: string, value: string }): void {
        this.addAction.emit(actionValue);
    }
}

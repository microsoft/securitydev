import { Component, Input, Output, EventEmitter } from '@angular/core';
// models
import { NetworkConnection } from 'src/app/models/graph';

@Component({
    selector: 'app-network-connection-item',
    templateUrl: './network-connection-item.component.html',
    styleUrls: ['./network-connection-item.component.css']
})
export class NetworkConnectionItemComponent {
    @Input() connection: NetworkConnection;
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

    public addToAction(name: string, value: string): void {
        this.addAction.emit({ name, value });
    }
}

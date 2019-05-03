import { Component, Input, Output, EventEmitter } from '@angular/core';
// models
import { CloudAppSecurityState } from 'src/app/models/graph';

@Component({
    selector: 'app-cloud-app-state-item',
    templateUrl: './cloud-app-state-item.component.html',
    styleUrls: ['./cloud-app-state-item.component.css']
})
export class CloudAppStateItemComponent {
    @Input() public item: CloudAppSecurityState;
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

import { Component, Input, Output, EventEmitter, HostListener } from '@angular/core';
// models
import { Alert } from 'src/app/models/graph';

@Component({
    selector: 'app-alert-item',
    templateUrl: './alert-item.component.html',
    styleUrls: ['./alert-item.component.css']
})
export class AlertItemComponent {
    @Input() public item: Alert;
    @Output() public itemSelect: EventEmitter<string>;

    @HostListener('click')
    onClick() { this.itemSelect.emit(this.item.id); }

    constructor() {
        this.itemSelect = new EventEmitter<string>();
    }
}

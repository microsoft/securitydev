import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'app-checkbox-group-item',
    templateUrl: './checkbox-group-item.component.html',
    styleUrls: ['./checkbox-group-item.component.css']
})
export class CheckboxGroupItemComponent {
    @Input() key: string;
    @Input() title: string;
    @Input() isLevel: boolean;
    @Input() checked: boolean;
    @Output() toggle: EventEmitter<string>;

    constructor() {
        this.toggle = new EventEmitter<string>();
    }

    toggleCheck() {
        // this.item.checked = !this.item.checked;
        this.toggle.emit(this.key);
    }
}

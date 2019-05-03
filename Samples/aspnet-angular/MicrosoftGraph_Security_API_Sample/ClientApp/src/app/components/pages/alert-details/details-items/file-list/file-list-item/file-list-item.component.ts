import { Component, Input, Output, EventEmitter, HostListener } from '@angular/core';
// models
import { FileSecurityState } from 'src/app/models/graph';

@Component({
    selector: 'app-file-list-item',
    templateUrl: './file-list-item.component.html',
    styleUrls: ['./file-list-item.component.css']
})
export class FileListItemComponent {
    @Input() public item: FileSecurityState;
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

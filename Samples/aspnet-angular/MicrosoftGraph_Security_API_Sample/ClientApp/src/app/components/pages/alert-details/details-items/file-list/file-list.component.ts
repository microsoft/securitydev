import { Component, Input, Output, EventEmitter } from '@angular/core';
// models
import { FileSecurityState } from 'src/app/models/graph';

@Component({
    selector: 'app-file-list',
    templateUrl: './file-list.component.html',
    styleUrls: ['./file-list.component.css']
})
export class FileListComponent {
    public title = 'Files';
    @Input() isExpanded = true;
    @Input() items: FileSecurityState[];
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

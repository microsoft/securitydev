import { Component, Input, HostBinding, Output, EventEmitter } from '@angular/core';
// models
import { BreadcrumbItem } from './breadcrumbs/breadcrumbs.component';

@Component({
    selector: 'app-content-header',
    templateUrl: './content-header.component.html',
    styleUrls: ['./content-header.component.css']
})
export class ContentHeaderComponent {
    @Input() breadcrumbItems: BreadcrumbItem[];
    @Input() title: string;
    @Input() isFiltersEnabled = false;
    @Output() filterClick: EventEmitter<void>;

    @HostBinding('class.page-content-linear-container') hostClass = true;

    public constructor() {
        this.filterClick = new EventEmitter<void>();
    }

    public onFilterClick(): void {
        this.filterClick.emit();
    }
}

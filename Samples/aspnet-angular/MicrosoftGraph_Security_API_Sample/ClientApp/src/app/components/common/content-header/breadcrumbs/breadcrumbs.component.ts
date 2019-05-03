import { Component, Input } from '@angular/core';

export interface BreadcrumbItem {
    Title: string;
    URL: string;
}

@Component({
    selector: 'app-breadcrumbs',
    templateUrl: './breadcrumbs.component.html',
    styleUrls: ['./breadcrumbs.component.css']
})
export class BreadcrumbsComponent {
    @Input() items: BreadcrumbItem[];
}

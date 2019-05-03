import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-alert-details-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.css']
})
export class AlertDetailsHeaderComponent {
    @Input() title: string;
    @Input() severity: string;
    @Input() status: string;
}

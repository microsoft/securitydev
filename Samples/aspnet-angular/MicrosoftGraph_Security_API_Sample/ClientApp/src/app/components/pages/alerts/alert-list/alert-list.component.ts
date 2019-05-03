import { Component, Input, HostBinding } from '@angular/core';
import { Router } from '@angular/router';
// models
import { Alert } from 'src/app/models/graph';

@Component({
    selector: 'app-alert-list',
    templateUrl: './alert-list.component.html',
    styleUrls: ['./alert-list.component.css']
})
export class AlertListComponent {
    public title = 'Matching Alerts';

    @Input() public alerts: Alert[];
    @HostBinding('class.page-content-linear-container') hostClass = true;

    constructor(private router: Router) { }

    public itemSelect(alertId: string): void {
        if (alertId) {
            // navigate to the alert details page
            this.router.navigate([`/alerts/${alertId}`]);
        }
    }
}

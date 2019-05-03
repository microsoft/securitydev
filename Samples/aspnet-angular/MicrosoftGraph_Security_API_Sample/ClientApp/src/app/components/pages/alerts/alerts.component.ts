import { Component, OnInit } from '@angular/core';
// models
import { Alert } from 'src/app/models/graph';
import { BreadcrumbItem } from '../../common/content-header/breadcrumbs/breadcrumbs.component';
import { Queries } from 'src/app/models/response';
// services
import { AlertService } from 'src/app/services/alert.service';
import { AlertFilterService } from '../../../services/alert-filter.service';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
    selector: 'app-alerts-page',
    templateUrl: './alerts.component.html',
    styleUrls: ['./alerts.component.css']
})
export class AlertsComponent implements OnInit {
    public title = 'matching alerts';
    public breadcrumbItems: BreadcrumbItem[] = [
        { Title: 'Dashboard', URL: '/dashboard' },
        { Title: 'Alerts', URL: '/alerts' }
    ];

    public alerts: Alert[];
    public queries: Queries;

    constructor(
        private alertFilterService: AlertFilterService,
        private alertService: AlertService,
        private loader: LoaderService
    ) {
        this.loader.Show('');
    }

    ngOnInit(): void {
        this.LoadAlerts();
    }

    public LoadAlerts(): void {
        this.loader.Show('');
        // get filters adopted to query
        const filtersForQuery = this.alertFilterService.ToFilterQuery();
        // get alerts by filters
        this.alertService.getAlertsByFilter(filtersForQuery).subscribe(response => {
            if (response) {
                this.alerts = response.alerts;
                this.queries = response.queries;
            }
            this.loader.Hide();
        });
    }
}

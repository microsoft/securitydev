import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
// models
import { AlertDetails } from 'src/app/models/graph';
import { BreadcrumbItem } from '../../common/content-header/breadcrumbs/breadcrumbs.component';
import { Queries } from 'src/app/models/response';
import { AlertUpdateModel, ActionCreateModel } from 'src/app/models/request';
// services
import { AlertService, AlertFilterService, ActionService } from 'src/app/services';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
    selector: 'app-alert-details-page',
    templateUrl: './alert-details.component.html',
    styleUrls: ['./alert-details.component.css']
})
export class AlertDetailsComponent implements OnInit {
    public title = 'matching alerts';
    public breadcrumbItems: BreadcrumbItem[] = [];

    public alertDetails: AlertDetails;
    public actionDetails: { name: string, value: string };
    public queries: Queries;

    constructor(
        private alertService: AlertService,
        private alertFilterService: AlertFilterService,
        private actionService: ActionService,
        private route: ActivatedRoute,
        private router: Router,
        private loader: LoaderService
    ) {
        this.loader.Show('');
    }

    ngOnInit(): void {
        this.getAlertDetails();
    }

    getAlertDetails(): void {
        this.loader.Show('');
        const alertId: string = this.route.snapshot.paramMap.get('id');
        this.alertService.getAlertDetails(alertId).subscribe(response => {
          if (response) {
                this.alertDetails = response.alertDetails;
                // reverse activity history
                this.alertDetails.historyStates = this.alertDetails.historyStates
                    ? this.alertDetails.historyStates.reverse()
                    : [];
                this.queries = response.queries;
                // fill breadcrumbs
                this.breadcrumbItems = [
                    { Title: 'Dashboard', URL: '/dashboard' },
                    { Title: 'Alerts', URL: '/alerts' },
                    { Title: 'Alert Details', URL: `/alerts/${this.alertDetails.id}` }
                ];
            }
            this.loader.Hide();
        });
    }

    updateAlert(alert: AlertUpdateModel): void {
        this.loader.Show('');
        if (alert) {
            this.alertService.updateAlert(alert).subscribe(response => {
                if (response) {
                    this.alertDetails = response.alertDetails;
                    // reverse activity history
                    this.alertDetails.historyStates = this.alertDetails.historyStates
                        ? this.alertDetails.historyStates.reverse()
                        : [];
                    this.queries = response.queries;
                }
                this.loader.Hide();
            });
        }
    }

    invokeAction(newAction: ActionCreateModel): void {
        // add alert id to action
        newAction.reason.alertId = this.alertDetails.id;
        // send request to create action
        this.actionService.createAction(newAction).subscribe(result => {
            this.router.navigate([`/actions/${newAction.reason.alertId}`]);
        });
    }

    filterAlert(filterValue: { key: string, value: string }): void {
        // set filters
        this.alertFilterService.SetFilter([filterValue]);
        // and navigate to the Alert list page
        this.router.navigate(['/alerts']);
    }

    setActionInfo(actionValue: { name: string, value: string }) {
        this.actionDetails = actionValue;
    }
}

import { Component, Input, HostBinding } from '@angular/core';
import { Router } from '@angular/router';
// models
import { Queries } from 'src/app/models/response';
// services
import { AlertFilterService, SubscriptionService } from 'src/app/services';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
    selector: 'app-query-list',
    templateUrl: './query-list.component.html',
    styleUrls: ['./query-list.component.css']
})
export class QueryListComponent {
    @Input() queries: Queries;
    @HostBinding('class.page-content-linear-container') hostClass = true;

    // have Object.keys accessible in the template and use it in *ngFor
    public objectKeys = Object.keys;

    constructor(
        private alertFilterService: AlertFilterService,
        private subscriptionService: SubscriptionService,
        private router: Router,
        private loader: LoaderService
    ) { }

    public subscribe(): void {
        this.loader.Show('');
        // get filters for new subscription
        const alertFilters = this.alertFilterService.ToFilterQuery();
        // send request to create subscription
        this.subscriptionService.createSubscription(alertFilters).subscribe(createdSubscription => {
            if (createdSubscription) {
                // redirect to the 'subscriptions page'
                this.router.navigate([`/subscriptions`], { state: { fromAlertsPage: true } });
            }
            this.loader.Hide();
        });
    }
}

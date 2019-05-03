import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
// environment
import { environment } from 'src/environments/environment';
// models
import { BreadcrumbItem } from '../../common/content-header/breadcrumbs/breadcrumbs.component';
import { Subscription } from 'src/app/models/graph/subscription.model';
import { SubscriptionSearchResult, Queries } from 'src/app/models/response';
// services
import { SubscriptionFilterService, SubscriptionService } from 'src/app/services';
import { LoaderService } from 'src/app/services/loader.service';

@Component({
    selector: 'app-subscriptions-page',
    templateUrl: './subscriptions.component.html',
    styleUrls: ['./subscriptions.component.css']
})
export class SubscriptionsComponent implements OnInit {
    public title = 'Subscriptions';
    public breadcrumbItems: BreadcrumbItem[] = [];
    public subscriptions: Subscription[] = [];
    public queries: Queries;

    constructor(
        private filterService: SubscriptionFilterService,
        private subscriptionService: SubscriptionService,
        private router: Router,
        private loader: LoaderService
    ) {
        this.loader.Show('');
        // get route state
        const state = this.router.getCurrentNavigation().extras.state || {};
        // init breadcrumbs
        this.breadcrumbItems = this.getBreadcrumbsByRouteState(state);
    }

    private getBreadcrumbsByRouteState(state: { [key: string]: any }): BreadcrumbItem[] {
        return state.fromAlertsPage
            ? [
                { Title: 'Dashboard', URL: '/dashboard' },
                { Title: 'Alerts', URL: '/alerts' },
                { Title: 'Subscriptions', URL: '/subscriptions' }
            ]
            : [
                { Title: 'Dashboard', URL: '/dashboard' },
                { Title: 'Subscriptions', URL: '/subscriptions' }
            ];
    }

    ngOnInit(): void {
        this.LoadSubscriptions();
    }

    public LoadSubscriptions(): void {
        this.loader.Show('');
        // get filters adopted to query
        const filtersForQuery = this.filterService.ToFilterQuery();
        // get alerts by filters
        this.subscriptionService.getSubscriptionsByFilter(filtersForQuery)
            .subscribe((response: SubscriptionSearchResult) => {
                if (response) {
                    if (response.subscriptions) {
                        // delete prefix '/security/alerts?$filter=' from all subscriptions to show only filter value
                        this.subscriptions = response.subscriptions.map(subscription => {
                            subscription.resource = subscription.resource
                                ? subscription.resource.replace('/security/alerts?$filter=', '')
                                : '';
                            return subscription;
                        })
                            // sort by expiration date time
                            .sort((s1, s2) => s1.expirationDateTime > s2.expirationDateTime
                                ? -1 : s1.expirationDateTime < s2.expirationDateTime
                                    ? 1
                                    : 0);
                    }
                    if (response.queries) {
                        this.queries = response.queries;
                    }
                }
                this.loader.Hide();
            });
    }

    public notificationButtonClick(): void {
        const baseUrl = environment.baseUrl.replace('/api', '');
        window.open(`${baseUrl}/notifications/list`, '_blank');
    }
}

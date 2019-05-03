import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import * as moment from 'moment';
import { map } from 'rxjs/operators';
// models
import { Subscription } from '../models/graph';
import { SubscriptionFilterData, AlertFilterData, SubscriptionSearchResult } from '../models/response';
// services
import { HttpService } from './http.service';

@Injectable({ providedIn: 'root' })
export class SubscriptionService {
    constructor(
        private http: HttpService) { }

    getSubscriptionsByFilter(filter: SubscriptionFilterData): Observable<SubscriptionSearchResult> {
        return this.http.get<SubscriptionSearchResult>('subscriptions')
            .pipe(
                map((allSubscriptions: SubscriptionSearchResult) => {
                    if (allSubscriptions) {
                        let filteredSubscriptions: Subscription[] = allSubscriptions.subscriptions;
                        // filter subscriptions
                        if (filter) {
                            if (filteredSubscriptions.length && filter['subscription:severity']) {
                                filteredSubscriptions = filteredSubscriptions.filter(subscription => filter['subscription:severity']
                                    .some(filterValues => subscription.resource.toLowerCase().indexOf(filterValues.toLowerCase()) > -1));
                            }
                            if (filteredSubscriptions.length && filter['subscription:status']) {
                                filteredSubscriptions = filteredSubscriptions.filter(subscription => filter['subscription:status']
                                    .some(filterValues => subscription.resource.toLowerCase().indexOf(filterValues.toLowerCase()) > -1));
                            }
                            if (filteredSubscriptions.length && filter['subscription:resource']) {
                                filteredSubscriptions = filteredSubscriptions.filter(subscription => filter['subscription:resource']
                                    .some(filterValues => subscription.resource.toLowerCase().indexOf(filterValues.toLowerCase()) > -1));
                            }
                            if (filteredSubscriptions.length && filter['subscription:expirationstartdatetime']) {
                                // filter by subscription created date time
                                filteredSubscriptions = filteredSubscriptions
                                    .filter(subscription => filter['subscription:createdstartdatetime']
                                        .some(filterValue => moment(subscription.expirationDateTime) > moment(filterValue)));
                            }
                            if (filteredSubscriptions.length && filter['subscription:expirationenddatetime']) {
                                // filter by subscription created date time
                                filteredSubscriptions = filteredSubscriptions
                                    .filter(subscription => filter['subscription:createdenddatetime']
                                        .some(filterValue => moment(subscription.expirationDateTime) < moment(filterValue)));
                            }
                        }
                        return {
                            queries: allSubscriptions.queries,
                            subscriptions: filteredSubscriptions
                        };
                    } else {
                        return allSubscriptions;
                    }
                })
            );
    }

    createSubscription(alertFilter: AlertFilterData): Observable<Subscription> {
        return this.http.post<Subscription>('subscriptions', alertFilter);
    }
}

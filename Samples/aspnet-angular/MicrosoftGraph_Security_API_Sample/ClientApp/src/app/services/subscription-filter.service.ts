import { Injectable } from '@angular/core';
import * as merge from 'deepmerge';
import * as moment from 'moment';
// models
import { SubscriptionFilterData } from '../models/response';
import { CheckboxItem } from 'src/app/components/common/inputs/checkbox-group/checkbox-group.component';
// services
import { AlertValuesService } from './alert-values.service';

@Injectable({ providedIn: 'root' })
export class SubscriptionFilterService {
    /*
        saves state for empty filters with data from server
        is needed to more quickly filters resetting
    */
    private initialFilters: SubscriptionFilterData;
    // current action filters state
    private subscriptionFilters: SubscriptionFilterData;

    constructor(private valuesService: AlertValuesService) {
        // initialize filters
        this.Initialize();
    }

    private get InitialFilters(): SubscriptionFilterData {
        return merge({}, this.initialFilters);
    }

    private set InitialFilters(value: SubscriptionFilterData) {
        this.initialFilters = merge({}, value);
    }

    get SubscriptionFilters(): SubscriptionFilterData {
        return this.subscriptionFilters;
    }

    set SubscriptionFilters(value: SubscriptionFilterData) {
        this.subscriptionFilters = value;
        // save to the localStorage
        if (value) {
            localStorage.setItem('subscriptionFilters', JSON.stringify(value));
        } else {
            localStorage.removeItem('subscriptionFilters');
        }
    }

    public ToFilterQuery(): SubscriptionFilterData {
        // copy filters
        const filters = merge({}, this.SubscriptionFilters);
        for (const filterKey in filters) {
            if (filters.hasOwnProperty(filterKey)) {
                // delete empty filters
                if (!filters[filterKey]) {
                    delete filters[filterKey];
                } else {
                    if (Array.isArray(filters[filterKey])) {
                        // if array is of CheckboxItem,it is need to transform array of CheckboxItems to array of string
                        if (filters[filterKey].length && filters[filterKey][0].title) {
                            // filter only checked items and get titles
                            filters[filterKey] = filters[filterKey]
                                .filter((checkboxItem: CheckboxItem) => checkboxItem.checked)
                                .map((checkboxItem: CheckboxItem) => checkboxItem.title);
                            // delete if array of checked items is empty
                            if (filters[filterKey].length < 1) {
                                delete filters[filterKey];
                            }
                        }
                    } else {
                        // ATTENTION PLEASE!!!!! all filters must be arrays of strings
                        // check if it is a moment datetime
                        if (moment.isMoment(filters[filterKey])) {
                            // get ISO string from moment object
                            filters[filterKey] = moment(filters[filterKey]).toISOString();
                        } else {
                            // any non-string value to string
                            if (typeof filters[filterKey] !== 'string' && filters[filterKey].toString) {
                                filters[filterKey] = filters[filterKey].toString();
                            }
                        }
                        // create array for any non-array filter
                        filters[filterKey] = [filters[filterKey]];
                    }
                }
            }
        }
        return filters;
    }

    public ResetFilters(): void {
        // immediately reset filters to initial state, which was loaded on when app is started
        this.SubscriptionFilters = this.InitialFilters;
    }

    public Initialize(): void {
        const actionFilterValues = this.valuesService.Values;

        const filters: SubscriptionFilterData = {};
        // process all filter properties, that have predefined values, prepare them to use in checkbox groups
        // process subscription severity
        filters['subscription:severity'] = actionFilterValues.alertSeverities
            .map((option: string): CheckboxItem => ({ title: option, value: option }));
        // process subscription status
        filters['subscription:status'] = actionFilterValues.alertStatuses
            .map((option: string): CheckboxItem => ({ title: option, value: option }));

        // save initial filters
        this.InitialFilters = filters;
        // check filters in the local storage
        const filterDataFromLocalStorage = localStorage.getItem('subscriptionFilters');
        this.SubscriptionFilters = filterDataFromLocalStorage
            // set saved filters from local storage
            ? JSON.parse(filterDataFromLocalStorage)
            // or initial filters
            : this.InitialFilters;
    }
}

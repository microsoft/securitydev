import { Injectable } from '@angular/core';
import * as merge from 'deepmerge';
import * as moment from 'moment';
// models
import { AlertFilterData } from '../models/response';
import { CheckboxItem } from 'src/app/components/common/inputs/checkbox-group/checkbox-group.component';
// services
import { AlertValuesService } from './alert-values.service';

@Injectable({ providedIn: 'root' })
export class AlertFilterService {
    /*
        saves state for empty filters with data from server
        is needed to more quickly filters resetting
    */
    private initialFilters: AlertFilterData;
    // current alert filters state
    private alertFilters: AlertFilterData;

    constructor(private valuesService: AlertValuesService) {
        // initialize filters
        this.Initialize();
    }

    private get InitialFilters(): AlertFilterData {
        return merge({}, this.initialFilters);
    }

    private set InitialFilters(value: AlertFilterData) {
        this.initialFilters = merge({}, value);
    }

    get AlertFilters(): AlertFilterData {
        return this.alertFilters;
    }

    set AlertFilters(value: AlertFilterData) {
        this.alertFilters = value;
        // save to the localStorage
        if (value) {
            localStorage.setItem('alertFilters', JSON.stringify(value));
        } else {
            localStorage.removeItem('alertFilters');
        }
    }

    public SetFilter(filterValues: { key: string, value: string }[]): void {
        if (filterValues && Array.isArray(filterValues)) {
            // reset all filters to initial state
            this.AlertFilters = this.InitialFilters;
            // set top = 50 by default
            this.AlertFilters['top'] = 50;
            // set another filters
            filterValues.forEach(filterValue => {
                // get current filter
                const filter = this.AlertFilters[filterValue.key];
                // check his type
                if (Array.isArray(filter)) {
                    // if it is an array, then check item type
                    if (filter[0] && filter[0].title && filter[0].value) {
                        // if it is an array of Checkbox items, find item to select
                        const itemToSelect: CheckboxItem | null = (<Array<CheckboxItem>>this.AlertFilters[filterValue.key])
                            .find(item => item.value === filterValue.value);
                        if (itemToSelect) {
                            itemToSelect.checked = true;
                        }
                    } else {
                        // set filter
                        this.AlertFilters[filterValue.key] = [filterValue.value];
                    }

                } else {
                    // set filter
                    this.AlertFilters[filterValue.key] = filterValue.value;
                }
            });
        }
    }

    public ToFilterQuery(): AlertFilterData {
        // copy filters
        const filters = merge({}, this.AlertFilters);
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
        delete filters['top'];
        return {
            Top: this.AlertFilters['top'],
            Filters: filters
        };
    }

    public ResetFilters(): void {
        // immediately reset filters to initial state, which was loaded on when app is started
        this.AlertFilters = this.InitialFilters;
    }

    public Initialize(): void {
        const alertFilterValues = this.valuesService.Values;

        const filters: AlertFilterData = {};
        // set default value for 'top'
        filters['top'] = 1;
        // process all filter properties, that have predefined values, prepare them to use in checkbox groups
        // process alert statuses
        filters['alert:status'] = alertFilterValues.alertStatuses
            .map((option: string): CheckboxItem => ({ title: option, value: option }));
        // process alert severity levels
        filters['alert:severity'] = alertFilterValues.alertSeverities
            .map((option: string): CheckboxItem =>
                ({ title: option, value: option.toLowerCase(), isLevel: true }));
        // process vendor providers
        filters['vendor:provider'] = alertFilterValues.alertProviders
            .map((option: string): CheckboxItem => ({ title: option, value: option }));
        // process alert categories
        filters['alert:category'] = alertFilterValues.alertCategories
            .map((option: string): CheckboxItem => ({ title: option, value: option }));
        // process alert feedbacks
        filters['alert:feedback'] = alertFilterValues.alertFeedbacks
            .map((option: string): CheckboxItem => ({ title: option, value: option }));

        // save initial filters
        this.InitialFilters = filters;
        // check filters in the local storage
        const filterDataFromLocalStorage = localStorage.getItem('alertFilters');
        this.AlertFilters = filterDataFromLocalStorage
            // set saved filters from local storage
            ? JSON.parse(filterDataFromLocalStorage)
            // or initial filters
            : this.InitialFilters;
    }
}

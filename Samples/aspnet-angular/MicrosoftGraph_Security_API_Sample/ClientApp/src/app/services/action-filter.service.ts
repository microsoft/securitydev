import { Injectable } from '@angular/core';
import * as merge from 'deepmerge';
import * as moment from 'moment';
// models
import { ActionFilterData } from '../models/response';
import { CheckboxItem } from 'src/app/components/common/inputs/checkbox-group/checkbox-group.component';
// services
import { ActionValuesService } from './action-values.service';

@Injectable({ providedIn: 'root' })
export class ActionFilterService {
    /*
        saves state for empty filters with data from server
        is needed to more quickly filters resetting
    */
    private initialFilters: ActionFilterData;
    // current action filters state
    private actionFilters: ActionFilterData;

    constructor(private valuesService: ActionValuesService) {
        // initialize filters
        this.Initialize();
    }

    private get InitialFilters(): ActionFilterData {
        return merge({}, this.initialFilters);
    }

    private set InitialFilters(value: ActionFilterData) {
        this.initialFilters = merge({}, value);
    }

    get ActionFilters(): ActionFilterData {
        return this.actionFilters;
    }

    set ActionFilters(value: ActionFilterData) {
        this.actionFilters = value;
        // save to the localStorage
        if (value) {
            localStorage.setItem('actionFilters', JSON.stringify(value));
        } else {
            localStorage.removeItem('actionFilters');
        }
    }

    public ToFilterQuery(): ActionFilterData {
        // copy filters
        const filters = merge({}, this.ActionFilters);
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
        this.ActionFilters = this.InitialFilters;
    }

    public Initialize(): void {
        const actionFilterValues = this.valuesService.Values;

        const filters: ActionFilterData = {};
        // process all filter properties, that have predefined values, prepare them to use in checkbox groups
        // process action name
        filters['action:name'] = actionFilterValues.actionNames
            .map((option: string): CheckboxItem => ({ title: option, value: option }));
        // process action target
        filters['action:target'] = actionFilterValues.actionTargets
            .map((option: string): CheckboxItem => ({ title: option, value: option }));
        // process action status
        filters['action:status'] = actionFilterValues.actionStatuses
            .map((option: string): CheckboxItem => ({ title: option, value: option }));
        // process action provider
        filters['action:provider'] = actionFilterValues.actionProviders
            .map((option: string): CheckboxItem => ({ title: option, value: option }));

        // save initial filters
        this.InitialFilters = filters;
        // check filters in the local storage
        const filterDataFromLocalStorage = localStorage.getItem('actionFilters');
        this.ActionFilters = filterDataFromLocalStorage
            // set saved filters from local storage
            ? JSON.parse(filterDataFromLocalStorage)
            // or initial filters
            : this.InitialFilters;
    }
}

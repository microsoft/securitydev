import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import * as moment from 'moment';
// models
import { Action } from '../models/graph/action.model';
import { ActionFilterData } from '../models/response';
// services
import { HttpService } from './http.service';
import { map } from 'rxjs/operators';
import { ActionCreateModel } from '../models/request';

@Injectable({ providedIn: 'root' })
export class ActionService {
    constructor(
        private http: HttpService) { }

    getActionsByFilter(filter: ActionFilterData): Observable<Action[]> {
        return this.http.get<Action[]>('actions')
            .pipe(
                map(allActions => {
                    let filteredAction = allActions;
                    // filter actions
                    if (filter) {
                        if (filter['action:name']) {
                            // filter by action type
                            filteredAction = filteredAction.filter(action => filter['action:name']
                                .map(item => item.toLowerCase())
                                .includes(action.name.toLowerCase()));
                            // if there ane no actions, return empty array
                            if (!filteredAction.length) {
                                return filteredAction;
                            }
                        }
                        if (filter['action:target']) {
                            // filter by action target
                            filteredAction = filteredAction.filter(action => filter['action:target']
                                .map(item => item.toLowerCase())
                                .includes(action.target.name.toLowerCase()));
                            // if there ane no actions, return empty array
                            if (!filteredAction.length) {
                                return filteredAction;
                            }
                        }
                        if (filter['action:status']) {
                            // filter by action status
                            filteredAction = filteredAction.filter(action => filter['action:status']
                                .map(item => item.toLowerCase())
                                .includes(action.status.toLowerCase()));
                            // if there ane no actions, return empty array
                            if (!filteredAction.length) {
                                return filteredAction;
                            }
                        }
                        if (filter['action:provider']) {
                            // filter by action provider
                            filteredAction = filteredAction.filter(action => action.securityVendorInformation && filter['action:provider']
                                .map(item => item.toLowerCase())
                                .includes(action.securityVendorInformation.provider.toLowerCase()));
                            // if there ane no actions, return empty array
                            if (!filteredAction.length) {
                                return filteredAction;
                            }
                        }
                        if (filter['action:submittedstartdatetime']) {
                            // filter by action submitted date time
                            filteredAction = filteredAction.filter(action => filter['action:submittedstartdatetime']
                                .some(filterValue => moment(action.submittedDateTime) > moment(filterValue)));
                            // if there ane no actions, return empty array
                            if (!filteredAction.length) {
                                return filteredAction;
                            }
                        }
                        if (filter['action:submittedenddatetime']) {
                            // filter by action submitted date time
                            filteredAction = filteredAction.filter(action => filter['action:submittedenddatetime']
                                .some(filterValue => moment(action.submittedDateTime) < moment(filterValue)));
                            // if there ane no actions, return empty array
                            if (!filteredAction.length) {
                                return filteredAction;
                            }
                        }
                    }
                    // return filtered action
                    return filteredAction;
                })
            );
    }

    createAction(action: ActionCreateModel): Observable<Action[]> {
        return this.http.post<Action[]>('actions', action);
    }
}

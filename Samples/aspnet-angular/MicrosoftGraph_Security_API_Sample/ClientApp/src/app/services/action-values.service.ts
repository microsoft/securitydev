import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import * as merge from 'deepmerge';
// models
import { ActionValues } from '../models/response';
// services
import { HttpService } from './http.service';

@Injectable({ providedIn: 'root' })
export class ActionValuesService {
    private values: ActionValues;

    constructor(private http: HttpService) {
        this.values = new ActionValues();
    }

    public get Values(): ActionValues {
        return merge({}, this.values);
    }

    Initialize(): Promise<void> {
        // anyway load data for filters from server
        return this.LoadFromServer()
            .toPromise()
            .then(response => {
                // save initial filters
                if (response) {
                    this.values = response;
                }
                return Promise.resolve();
            })
            .catch((error) => Promise.resolve());
    }

    private LoadFromServer(): Observable<ActionValues> {
        return this.http.get<ActionValues>('actions/filters');
    }
}

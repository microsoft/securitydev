import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import * as merge from 'deepmerge';
// models
import { AlertValues } from '../models/response';
// services
import { HttpService } from './http.service';

@Injectable({ providedIn: 'root' })
export class AlertValuesService {
    private values: AlertValues;

    constructor(private http: HttpService) {
        this.values = new AlertValues();
    }

    public get Values(): AlertValues {
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

    private LoadFromServer(): Observable<AlertValues> {
        return this.http.get<AlertValues>('alerts/filters');
    }
}

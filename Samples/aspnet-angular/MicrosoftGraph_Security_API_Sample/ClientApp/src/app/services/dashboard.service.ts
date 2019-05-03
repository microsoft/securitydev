import { Injectable } from '@angular/core';

import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';


// models
import { AlertStatistics } from '../models/graph/alert-statistic.model';
// services
import { HttpService } from './http.service';

@Injectable({ providedIn: 'root' })
export class DashboardService {
    constructor(
        private http: HttpService) { }

        getAlertStatistics(): Observable<AlertStatistics> {
            return this.http.get<AlertStatistics>(`alerts/Statistics/200`);
        }
}

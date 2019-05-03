import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

// models
import { AlertSearchResult, AlertDetailsResult } from '../models/response';
import { AlertFilterData } from '../models/response';
// services
import { HttpService } from './http.service';
import { AlertUpdateModel } from '../models/request';

@Injectable({ providedIn: 'root' })
export class AlertService {
    constructor(
        private http: HttpService) { }

    getAlertsByFilter(filter: AlertFilterData): Observable<AlertSearchResult> {
        return this.http.post<AlertSearchResult>('alerts', filter);
    }

    getAlertDetails(id: string): Observable<AlertDetailsResult> {
        return this.http.get<AlertDetailsResult>(`alerts/${id}`);
    }

    updateAlert(alert: AlertUpdateModel): Observable<AlertDetailsResult> {
        return this.http.patch(`alerts/${alert.id}`, alert);
    }
}

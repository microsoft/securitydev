import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import * as queryString from 'query-string';

@Injectable({ providedIn: 'root' })
export class HttpService {
    constructor(
        private http: HttpClient) { }

    get<T>(path: string, data?: any): Observable<T> {
        // create url
        const url = data
            // serialize data object to query params
            ? `${environment.baseUrl}/${path}?${queryString.stringify(data)}`
            // use only path
            : `${environment.baseUrl}/${path}`;
        return this.http.get<T>(url)
            .pipe(catchError(this.handleError(path, null)));
    }

    post<T>(path: string, data: any): Observable<T> {
        const url = `${environment.baseUrl}/${path}`;
        return this.http.post<T>(url, data)
            .pipe(catchError(this.handleError(path, null)));
    }

    patch<T>(path: string, data: any): Observable<T> {
        const url = `${environment.baseUrl}/${path}`;
        return this.http.patch<T>(url, data)
            .pipe(catchError(this.handleError(path, null)));
    }

    private handleError<T>(operation = 'operation', result?: T) {
        return (error: any): Observable<T> => {
            if (!error.ok) {
                switch (error.status) {
                    case 401: {
                        // redirect to login page
                    } break;
                    default: {

                    }
                }
            }
            // Let the app keep running by returning an empty result.
            return of(result as T);
        };
    }
}

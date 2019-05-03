import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

// models
import { SecureScoreDetailsResponse } from '../models/response';

// services
import { HttpService } from './http.service';


@Injectable({ providedIn: 'root' })
export class SecureScoreService {
  constructor(
    private http: HttpService) { }

  getSecureScoreDetails(): Observable<SecureScoreDetailsResponse> {
    return this.http.get<SecureScoreDetailsResponse>('securescores/GetSecureDetails');
  }
}

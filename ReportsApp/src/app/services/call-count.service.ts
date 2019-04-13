import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { DashboardCallCount } from '../models/dashboard-call-count.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable()
export class CallCountService {
    constructor(private http: HttpClient) {

    }

    getAll(): Observable<DashboardCallCount> {
        return this.http.get<DashboardCallCount>(`${environment.apiUrl}/CallCount/Dashboard`);
    }
}

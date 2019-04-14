import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { TableQueryOptions } from '../models/table-query-options.model';
import { CallDatasourceModel } from '../models/call-datasource.model';

@Injectable()
export class CallService {
    constructor(private http: HttpClient) {

    }

    get(queryOptions: TableQueryOptions): Observable<CallDatasourceModel> {
        return this.http.post<CallDatasourceModel>(`${environment.apiUrl}/Call/GetAll`, queryOptions);
    }
}

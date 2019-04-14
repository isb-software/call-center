import { CollectionViewer, DataSource } from '@angular/cdk/collections';
import { CallService } from '../services/call.service';
import { Call } from '../models/call.model';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { TableQueryOptions } from '../models/table-query-options.model';

export class CallsDataSource implements DataSource<Call> {
    private callsSubject = new BehaviorSubject<Call[]>([]);
    private loadingSubject = new BehaviorSubject<boolean>(false);
    private totalRecordsSubject = new BehaviorSubject<number>(0);

    public loading$ = this.loadingSubject.asObservable();
    public totalRecords$ = this.totalRecordsSubject.asObservable();

    constructor(private callService: CallService) {
    }

    connect(collectionViewer: CollectionViewer): Observable<Call[]> {
        return this.callsSubject.asObservable();
    }

    disconnect(collectionViewer: CollectionViewer): void {
        this.callsSubject.complete();
        this.loadingSubject.complete();
        this.totalRecordsSubject.complete();
    }

    get(queryOptions: TableQueryOptions) {
        this.loadingSubject.next(true);

        this.callService.get(queryOptions)
            .subscribe(callDatasource => {
                if (callDatasource) {
                    this.callsSubject.next(callDatasource.Calls);
                    this.totalRecordsSubject.next(callDatasource.TotalRecords);
                } else {
                    this.callsSubject.next([]);
                    this.totalRecordsSubject.next(0);
                }
                this.loadingSubject.next(false);
            });
    }
}

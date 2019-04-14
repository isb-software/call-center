import { Component, OnInit, ViewChild, ElementRef, AfterViewInit } from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { TableQueryOptions } from 'src/app/models/table-query-options.model';
import { CallsDataSource } from 'src/app/datasource/call.datasource';
import { CallService } from 'src/app/services/call.service';
import { fromEvent, merge } from 'rxjs';
import { distinctUntilChanged, debounceTime, tap } from 'rxjs/operators';

@Component({
  selector: 'app-call-overview',
  templateUrl: './call-overview.component.html',
  styleUrls: ['./call-overview.component.scss']
})
export class CallOverviewComponent implements OnInit, AfterViewInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild('filterInput') filterInput: ElementRef;

  private tableQueryOptions: TableQueryOptions;

  callsDataSource: CallsDataSource;
  totalRecords = 0;

  displayedColumns = ['UserName', 'PersonName', 'PhoneNumber', 'StatusName',
    'County', 'City', 'Education', 'Age', 'DateTimeOfCall', 'Notes'];

  constructor(private callService: CallService) { }

  ngOnInit() {
    this.initializeTable();
  }

  ngAfterViewInit() {
    // event listener to filter input
    fromEvent(this.filterInput.nativeElement, 'keyup')
      .pipe(
        debounceTime(250),
        distinctUntilChanged(),
        tap(() => {
          this.paginator.pageIndex = 0;
          this.getCalls();
        })
      )
      .subscribe();

    // reset page when sorting
    this.sort.sortChange.subscribe(() => this.paginator.pageIndex = 0);

    // event listeners to on sort and on page change
    merge(this.sort.sortChange, this.paginator.page)
      .pipe(
        tap(() => this.getCalls())
      )
      .subscribe();
  }

  private getCalls() {
    this.tableQueryOptions.Limit = this.paginator.pageSize;
    this.tableQueryOptions.Offset = this.paginator.pageIndex === 0 ? 0 : this.paginator.pageIndex * this.paginator.pageSize;
    this.tableQueryOptions.SortBy = this.sort.active;
    this.tableQueryOptions.SortType = this.sort.direction;
    this.tableQueryOptions.SearchTerm = this.filterInput.nativeElement.value;
    this.callsDataSource.get(this.tableQueryOptions);
  }

  private initializeTable() {
    this.callsDataSource = new CallsDataSource(this.callService);
    this.tableQueryOptions = new TableQueryOptions();
    this.tableQueryOptions.SortBy = 'DateTimeOfCall';
    this.tableQueryOptions.SortType = 'desc';

    this.callsDataSource.get(this.tableQueryOptions);
    this.callsDataSource.totalRecords$.subscribe(totalRecords => this.totalRecords = totalRecords);
  }
}

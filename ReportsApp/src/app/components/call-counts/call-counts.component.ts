import { Component, OnInit } from '@angular/core';
import { CallCountService } from 'src/app/services/call-count.service';
import { DashboardCallCount } from 'src/app/models/dashboard-call-count.model';
import { StatusEnum } from 'src/app/models/status.model';

@Component({
  selector: 'app-call-counts',
  templateUrl: './call-counts.component.html',
  styleUrls: ['./call-counts.component.scss']
})
export class CallCountsComponent implements OnInit {
  dashboardCallCount: DashboardCallCount;
  StatusEnum = StatusEnum;

  constructor(private callCountService: CallCountService) { }

  ngOnInit() {
    this.callCountService.getAll().subscribe((result) => this.dashboardCallCount = result);
  }

  public getDailyCallsByStatus(statusEnum: StatusEnum): number {
    const dailyCalls = this.dashboardCallCount.DailyCalls.find(x => x.StatusId === statusEnum);
    return dailyCalls ? dailyCalls.Count : 0;
  }

  public getWeeklyCallsByStatus(statusEnum: StatusEnum): number {
    const weeklyCalls = this.dashboardCallCount.WeeklyCalls.find(x => x.StatusId === statusEnum);
    return weeklyCalls ? weeklyCalls.Count : 0;
  }

  public getMonthlyCallsByStatus(statusEnum: StatusEnum): number {
    const monthlyCalls = this.dashboardCallCount.MonthlyCalls.find(x => x.StatusId === statusEnum);
    return monthlyCalls ? monthlyCalls.Count : 0;
  }

  public getYearlyCallsByStatus(statusEnum: StatusEnum): number {
    const yearlyCalls = this.dashboardCallCount.YearlyCalls.find(x => x.StatusId === statusEnum);
    return yearlyCalls ? yearlyCalls.Count : 0;
  }
}

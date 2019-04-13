import { CallCount } from './call-count.model';

export interface DashboardCallCount {
    DailyCalls: CallCount[];
    WeeklyCalls: CallCount[];
    MonthlyCalls: CallCount[];
    YearlyCalls: CallCount[];
}

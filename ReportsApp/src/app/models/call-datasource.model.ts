import { Call } from './call.model';

export interface CallDatasourceModel {
    Calls: Call[];
    TotalRecords: number;
}

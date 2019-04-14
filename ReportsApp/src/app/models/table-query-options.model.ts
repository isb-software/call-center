export class TableQueryOptions {
    Offset?: number;
    Limit?: number;
    SearchTerm?: string;
    SortBy?: string;
    SortType?: string;

    constructor() {
        this.Offset = 0;
        this.Limit = 10;
        this.SearchTerm = null;
        this.SortBy = null;
        this.SortType = null;
    }
}

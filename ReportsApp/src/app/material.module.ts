import { NgModule } from '@angular/core';
import {
    MatButtonModule,
    MatCardModule,
    MatDividerModule,
    MatIconModule,
    MatListModule,
    MatSidenavModule,
    MatToolbarModule,
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatFormFieldModule,
    MatInputModule,
} from '@angular/material';

@NgModule({
    imports: [
        MatButtonModule,
        MatSidenavModule,
        MatToolbarModule,
        MatListModule,
        MatIconModule,
        MatCardModule,
        MatDividerModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        MatProgressSpinnerModule,
        MatFormFieldModule,
        MatInputModule
    ],
    exports: [
        MatButtonModule,
        MatSidenavModule,
        MatToolbarModule,
        MatListModule,
        MatIconModule,
        MatCardModule,
        MatDividerModule,
        MatTableModule,
        MatPaginatorModule,
        MatSortModule,
        MatProgressSpinnerModule,
        MatFormFieldModule,
        MatInputModule
    ],
})
export class MaterialModule { }

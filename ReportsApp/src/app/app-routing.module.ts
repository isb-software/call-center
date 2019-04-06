import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CallCountsComponent } from './components/call-counts/call-counts.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'dashboard' },

  { path: 'dashboard', component: CallCountsComponent },

  { path: '**', redirectTo: 'dashboard' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

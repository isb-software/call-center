import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MaterialModule } from './material.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CallCountsComponent } from './components/call-counts/call-counts.component';
import { CallCountService } from './services/call-count.service';
import { HttpClientModule } from '@angular/common/http';
import { CallOverviewComponent } from './components/call-overview/call-overview.component';
import { ReactiveFormsModule } from '@angular/forms';
import { CallService } from './services/call.service';

@NgModule({
  declarations: [
    AppComponent,
    CallCountsComponent,
    CallOverviewComponent
  ],
  imports: [
    BrowserModule,
    MaterialModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [
    CallCountService,
    CallService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

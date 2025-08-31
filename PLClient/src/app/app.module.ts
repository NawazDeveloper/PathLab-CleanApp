import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CbcListComponent } from './components/cbc/cbc-list/cbc-list.component';
import { CbcFormComponent } from './components/cbc/cbc-from/cbc-form.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    CbcListComponent,
    CbcFormComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
     HttpClientModule,   // ✅ add this
    AppRoutingModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }

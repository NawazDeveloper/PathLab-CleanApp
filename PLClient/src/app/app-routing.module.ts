import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CbcListComponent } from './components/cbc/cbc-list/cbc-list.component'
import{DashboardComponent} from './components/dashboard/dashboard.component'


const routes: Routes = [
  { path: '', redirectTo: '/cbc-list', pathMatch: 'full' }, // default route
  { path: 'cbc-list', component: CbcListComponent },
    { path: 'dashboard', component: DashboardComponent },
  // later you can add more routes like:
  // { path: 'cbc-form', component: CbcFormComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

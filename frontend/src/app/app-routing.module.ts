import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {StatsComponent} from './stats/stats.component';
import {CalendarComponent} from './calendar/calendar.component';
import {SettingsComponent} from './settings/settings.component';

const routes: Routes = [
  {path: '', loadChildren: () => import('./homepage/homepage.module').then(m => m.HomepageModule)},
  {
    path: 'dashboard',
    loadChildren: () => import('./dashboard/dashboard.module').then(m => m.DashboardModule)
  },
  {path: 'stats', component: StatsComponent},
  {path: 'calendar', component: CalendarComponent},
  {path: 'settings', component: SettingsComponent},
  {
    path: 'auth',
    loadChildren: () => import('./authentication/authentication.module').then(m => m.AuthenticationModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {
}

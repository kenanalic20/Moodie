import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StatsComponent } from './stats/stats.component';
import { CalendarComponent } from './calendar/calendar.component';
import { SettingsComponent } from './settings/settings.component';
import { ExportComponent } from './export/export.component';
import { GoalsComponent } from './goals/goals.component';
import { AuthGuard } from './auth.guard';
import { AchievementsComponent } from 'src/app/achievements/achievements.component';
import { UserInfoComponent } from './user-info/user-info.component';
import { HabitsComponent } from 'src/app/habits/habits.component';

const routes: Routes = [
    {
        path: '',
        loadChildren: () =>
            import('./homepage/homepage.module').then(m => m.HomepageModule),
    },
    {
        path: 'dashboard',
        loadChildren: () =>
            import('./dashboard/dashboard.module').then(m => m.DashboardModule),
        canActivate: [AuthGuard],
    },
    { path: 'stats', component: StatsComponent, canActivate: [AuthGuard] },
    {
        path: 'calendar',
        component: CalendarComponent,
        canActivate: [AuthGuard],
    },
    {
        path: 'settings',
        component: SettingsComponent,
        canActivate: [AuthGuard],
    },
    { path: 'export', component: ExportComponent, canActivate: [AuthGuard] },
    {
        path: 'achievements',
        component: AchievementsComponent,
        canActivate: [AuthGuard],
    },
    {
        path: 'user-info',
        component: UserInfoComponent,
        canActivate: [AuthGuard],
    },
    { path: 'habits', component: HabitsComponent, canActivate: [AuthGuard] },
    {
        path: 'auth',
        loadChildren: () =>
            import('./authentication/authentication.module').then(
                m => m.AuthenticationModule
            ),
    },
    { path: 'goals', component: GoalsComponent, canActivate: [AuthGuard] },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule {}

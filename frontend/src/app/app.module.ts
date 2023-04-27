import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './components/app.component';
import { ResetComponent } from './components/reset/reset.component';
import { RegisterComponent } from './components/register/register.component';
import { RouterOutlet } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { FrontpageComponent } from './components/frontpage/frontpage.component';
import { AboutComponent } from './components/about/about.component';
import { HeaderComponent } from './components/header/header.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LoginComponent } from './components/login/login.component';
import { StatsComponent } from './components/stats/stats.component';
import { CalendarComponent } from './components/calendar/calendar.component';
import { SettingsComponent } from './components/settings/settings.component';
import { MoodsComponent } from './components/dashboard/moods/moods.component';

@NgModule({
  declarations: [
    AppComponent,
    ResetComponent,
    RegisterComponent,
    FrontpageComponent,
    AboutComponent,
    HeaderComponent,
    DashboardComponent,
    LoginComponent,
    StatsComponent,
    CalendarComponent,
    SettingsComponent,
    MoodsComponent,
  ],
  imports: [BrowserModule, RouterOutlet, AppRoutingModule, FontAwesomeModule],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}

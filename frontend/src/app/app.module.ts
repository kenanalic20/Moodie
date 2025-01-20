import { isDevMode, NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { AppComponent } from "./app.component";
import { RouterOutlet } from "@angular/router";
import { AppRoutingModule } from "./app-routing.module";
import { StatsComponent } from "./stats/stats.component";
import { CalendarComponent } from "./calendar/calendar.component";
import { SettingsComponent } from "./settings/settings.component";
import { AuthenticationModule } from "./authentication/authentication.module";
import { DashboardModule } from "./dashboard/dashboard.module";
import { HomepageModule } from "./homepage/homepage.module";
import { HeaderModule } from "./header/header.module";
import { ServiceWorkerModule } from "@angular/service-worker";
import { StatGraphComponent } from "./stats/stat-graph/stat-graph.component";
import { StatActivitiesComponent } from "./stats/stat-activities/stat-activities.component";
import { SettingsModule } from "./settings/settings.module";
import { CalendarModule } from "./calendar/calendar.module";
import { HttpClientModule } from "@angular/common/http";
import { ExportComponent } from "./export/export.component";
import { ToastrModule } from "ngx-toastr";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { GoalsComponent } from "./goals/goals.component";
import { GoalFilterPipe } from "./pipes/goal-filter.pipe";

import {TranslateLoader, TranslateModule} from '@ngx-translate/core';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import { HttpClient, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { LanguageSwitcherComponent } from "./language-switcher/language-switcher.component";
import { SharedModule } from "./shared/shared.module";
@NgModule({
	declarations: [
		AppComponent,
		StatsComponent,
		SettingsComponent,
		StatGraphComponent,
		StatActivitiesComponent,
		ExportComponent,
		GoalsComponent,
		GoalFilterPipe
	],
	imports: [
		BrowserModule,
		RouterOutlet,
		AppRoutingModule,
		AuthenticationModule,
		DashboardModule,
		HomepageModule,
		HeaderModule,
		CalendarModule,
		ServiceWorkerModule.register("ngsw-worker.js", {
			enabled: !isDevMode(),
			// Register the ServiceWorker as soon as the application is stable
			// or after 30 seconds (whichever comes first).
			registrationStrategy: "registerWhenStable:30000",
		}),
		SettingsModule,
		HttpClientModule,
		ToastrModule.forRoot(),
		BrowserAnimationsModule,
		TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: HttpLoaderFactory,
                deps: [HttpClient]
            }
        }),
		// SharedModule
	],
	providers: [
		{
			provide: "API_URL",
			useValue: "http://localhost:5000/api",
		},
		provideHttpClient(withInterceptorsFromDi()),
	],
	bootstrap: [AppComponent],
})
export class AppModule {}

export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
    return new TranslateHttpLoader(http);
}
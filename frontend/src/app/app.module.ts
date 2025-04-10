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
import { ToastrModule } from "ngx-toastr";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HabitsComponent } from "./habits/habits.component";
import { GoalsComponent } from "./goals/goals.component";
import { GoalFilterPipe } from "./pipes/goal-filter.pipe";
import { FormsModule } from "@angular/forms";

import { TranslateLoader, TranslateModule } from "@ngx-translate/core";
import { TranslateHttpLoader } from "@ngx-translate/http-loader";
import { HttpClient, provideHttpClient, withInterceptorsFromDi } from "@angular/common/http";
import { AchievementsComponent } from "src/app/achievements/achievements.component";
import { ExportModule } from "./export/export.module";
import { UserInfoModule } from "./user-info/user-info.module";
import { StatsModule } from "./stats/stats.module";

@NgModule({
	declarations: [
		AppComponent,
		StatsComponent,
		SettingsComponent,
		StatGraphComponent,
		StatActivitiesComponent,
		AchievementsComponent,
		GoalsComponent,
		HabitsComponent,
		GoalFilterPipe,
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
		StatsModule,
		ServiceWorkerModule.register("ngsw-worker.js", {
			enabled: !isDevMode(),
			// Register the ServiceWorker as soon as the application is stable
			// or after 30 seconds (whichever comes first).
			registrationStrategy: "registerWhenStable:30000",
		}),
		SettingsModule,
		HttpClientModule,
		BrowserAnimationsModule, // This needs to be imported before ToastrModule
		ToastrModule.forRoot({
			positionClass: "toast-bottom-right",
			preventDuplicates: true,
			closeButton: true,
			progressBar: true,
			timeOut: 5000,
			extendedTimeOut: 2000,
			enableHtml: true,
			tapToDismiss: true,
			maxOpened: 5,
			autoDismiss: true,
			newestOnTop: true,
			iconClasses: {
				error: "toast-error",
				info: "toast-info",
				success: "toast-success",
				warning: "toast-warning",
			},
		}),
		FormsModule,
		TranslateModule.forRoot({
			loader: {
				provide: TranslateLoader,
				useFactory: HttpLoaderFactory,
				deps: [HttpClient],
			},
			defaultLanguage: "en",
		}),
		ExportModule,
		UserInfoModule,
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
	return new TranslateHttpLoader(http, "./assets/i18n/", ".json");
}

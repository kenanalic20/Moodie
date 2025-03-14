import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { LoginComponent } from "./login/login.component";
import { RegisterComponent } from "./register/register.component";
import { ResetComponent } from "./reset/reset.component";
import { AlreadyLoggedInGuard } from "../guards/already-logged-in.guard";

const routes: Routes = [
	{
		path: "",
		redirectTo: "login",
		pathMatch: "full",
	},
	{
		path: "login",
		component: LoginComponent,
		canActivate: [AlreadyLoggedInGuard],
	},
	{
		path: "register",
		component: RegisterComponent,
		canActivate: [AlreadyLoggedInGuard],
	},
	{
		path: "reset",
		component: ResetComponent,
		canActivate: [AlreadyLoggedInGuard],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class AuthenticationRoutingModule {}

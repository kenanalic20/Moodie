import { Component } from "@angular/core";
import { faGoogle } from "@fortawesome/free-brands-svg-icons";
import { isDev } from "../../globals";
import { faCode } from "@fortawesome/free-solid-svg-icons";
import { AuthService } from "src/app/services/auth.service";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { SettingsService } from "src/app/services/settings.service";
import { TranslateService } from "@ngx-translate/core";
import { LanguageService } from "src/app/services/language.service";

@Component({
	selector: "app-login",
	templateUrl: "./login.component.html",
})
export class LoginComponent {
	constructor(
		private authService: AuthService,
		private router: Router,
		private toastrService: ToastrService,
		private settingsService:SettingsService,
		private translateService:TranslateService,
		private languageService:LanguageService
	) {}
	faGoogle = faGoogle;
	isDevelopment = isDev;
	codeIcon = faCode;
	emailRegex = /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/;
	email: string = "";
	password: string = "";
	verificationCode: string = "";
	hide = true;
	hideLogin = false;
	hideVerification = true;

	onEmailChange(email: any) {
		this.email = email;
		console.log(email)
	}

	onPasswordChange(password: any) {
		this.password = password;
	}

	onVerificationCodeChange(verificationCode: any) {
		this.verificationCode = verificationCode;
	}

	login() {
		if (this.password === "" || this.email === "" || !this.email.match(this.emailRegex) || this.password.length < 8) {
			this.toastrService.error("Please enter valid credentials", "Error");
			return;
		}

		this.authService.login(this.email, this.password).subscribe(
			(response: any) => {
				// Check response message to determine if 2FA is required
				if (response.message === "Verification code sent to your email") {
					// 2FA is required - show verification input
					this.toastrService.success("Check your email for verification code", "Success");
					this.hide = false;
					this.hideLogin = true;
					this.hideVerification = false;
				} else if (response.message === "Login successful") {
					// 2FA not required - redirect directly to dashboard
					this.toastrService.success("Logged in successfully", "Success");
					
					this.settingsService.getSettings().subscribe(
						(settings: any) => {
							this.languageService.getLanguageById(settings.languageId).subscribe((language:any)=>{
								const preferredLanguage = language.code || 'en'; 
								this.translateService.use(preferredLanguage); 
								localStorage.setItem('Language', preferredLanguage);
							})
						},
						(error) => {
						  console.error('Error fetching user settings:', error);
						  this.toastrService.error("Failed to fetch user settings", "Error");
						}
					  );
					
					this.router.navigate(["/dashboard"]);
				}
			},
			(error) => {
				this.toastrService.error(error.error, "Error");
			},
		);
	}

	submitVerificationCode() {
		if (this.verificationCode === "") {
			this.toastrService.error("Please enter valid verification code", "Error");
			return;
		}
		this.authService.login(this.email, this.password, this.verificationCode).subscribe((response) => {
			this.toastrService.success("Logged in", "Success");
			this.router.navigate(["/dashboard"]);
		});
	}
}

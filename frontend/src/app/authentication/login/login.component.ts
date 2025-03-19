import { Component, OnInit } from "@angular/core";
import { faGoogle } from "@fortawesome/free-brands-svg-icons";
import { isDev } from "../../globals";
import { faCode } from "@fortawesome/free-solid-svg-icons";
import { AuthService } from "src/app/services/auth.service";
import { Router, ActivatedRoute } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { SettingsService } from "src/app/services/settings.service";
import { TranslateService } from "@ngx-translate/core";
import { LanguageService } from "src/app/services/language.service";

@Component({
	selector: "app-login",
	templateUrl: "./login.component.html",
})
export class LoginComponent implements OnInit {
	constructor(
		private authService: AuthService,
		private router: Router,
		private toastrService: ToastrService,
		private settingsService: SettingsService,
		private translateService: TranslateService,
		private languageService: LanguageService,
		private route: ActivatedRoute,
	) {}
	faGoogle = faGoogle;
	isDevelopment = isDev;
	codeIcon = faCode;
	emailRegex = /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/;
	email = "";
	password = "";
	verificationCode = "";
	hide = true;
	hideLogin = false;
	hideVerification = true;

	ngOnInit() {
		setTimeout(() => {
			const input = document.getElementById("email") as HTMLInputElement;
			const emailFromQuery = this.route.snapshot.queryParamMap.get("email");
			if (emailFromQuery && input) {
				this.email = emailFromQuery;
				input.value = emailFromQuery;
				input.focus();
			}
		}, 100);
	}

	onEmailChange(email: string) {
		this.email = email;
	}

	onPasswordChange(password: string) {
		this.password = password;
	}

	onVerificationCodeChange(verificationCode: string) {
		this.verificationCode = verificationCode;
	}

	login() {
		if (this.password === "" || this.email === "" || !this.email.match(this.emailRegex) || this.password.length < 8) {
			this.toastrService.error(
				this.translateService.instant("Please enter valid credentials"),
				this.translateService.instant("Error"),
			);
			return;
		}

		this.authService.login(this.email, this.password).subscribe(
			(response: any) => {
				// Check response message to determine if 2FA is required
				if (response.message === "Verification code sent to your email") {
					// 2FA is required - show verification input
					this.toastrService.success(
						this.translateService.instant("Check your email for verification code"),
						this.translateService.instant("Success"),
					);
					this.hide = false;
					this.hideLogin = true;
					this.hideVerification = false;
				} else if (response.message === "Login successful") {
					// 2FA not required - redirect directly to dashboard
					this.toastrService.success(
						this.translateService.instant("Logged in successfully"),
						this.translateService.instant("Success"),
					);

					this.settingsService.getSettings().subscribe((settings: any) => {
						this.languageService.getLanguageById(settings.languageId).subscribe((language: any) => {
							const preferredLanguage = language.code || "en";
							this.translateService.use(preferredLanguage);
							localStorage.setItem("Language", preferredLanguage);
						});
					});

					this.router.navigate(["/dashboard"]);
				}
			},
			(error: any) => {
				if (error.error === "Email not verified. Please check your email for verification link.") {
					this.toastrService.error(
						this.translateService.instant("Email not verified. Please check your inbox for verification link."),
						this.translateService.instant("Verification Required"),
					);
				} else {
					this.toastrService.error(error.error, this.translateService.instant("Error"));
				}
			},
		);
	}

	submitVerificationCode() {
		if (this.verificationCode === "") {
			this.toastrService.error(
				this.translateService.instant("Please enter valid verification code"),
				this.translateService.instant("Error"),
			);
			return;
		}

		this.authService.login(this.email, this.password, this.verificationCode).subscribe(
			(response: any) => {
				this.toastrService.success(
					this.translateService.instant("Logged in successfully"),
					this.translateService.instant("Success"),
				);
				this.router.navigate(["/dashboard"]);
			},
			(error: any) => {
				this.toastrService.error(error.error, this.translateService.instant("Error"));
			},
		);
	}

	resendVerificationEmail() {
		if (!this.email || !this.email.match(this.emailRegex)) {
			this.toastrService.error(
				this.translateService.instant("Please enter a valid email first"),
				this.translateService.instant("Error"),
			);
			return;
		}

		this.authService.resendVerificationEmail(this.email).subscribe(
			(response: any) => {
				this.toastrService.success(
					this.translateService.instant("Verification email sent. Please check your inbox."),
					this.translateService.instant("Success"),
				);
			},
			(error: any) => {
				this.toastrService.error(
					this.translateService.instant(error.error || "An error occurred"),
					this.translateService.instant("Error"),
				);
			},
		);
	}
}

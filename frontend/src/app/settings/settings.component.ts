import { Component, OnInit } from "@angular/core";
import { LanguageService } from "../services/language.service";
import { SettingsService } from "../services/settings.service";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";

@Component({
	selector: "app-settings",
	templateUrl: "./settings.component.html",
})
export class SettingsComponent implements OnInit {
	constructor(
		private languageService: LanguageService,
		private settingsService: SettingsService,
		private toastrService: ToastrService,
		private translateService: TranslateService,
	) {}
	languages: Array<{ id: number; code: string; name: string; region: string }> = [];
	languageOptions: Array<{ label: string; value: string }> = [];
	settings: any = {};
	currentTheme: string = localStorage.getItem("Theme") || "Light";
	currentLanguage: string = localStorage.getItem("Language") || "en";
	twoFactorEnabled: boolean = false;

	ngOnInit() {
		this.languageService.getLanguage().subscribe((data: any) => {
			this.languages = data;
			this.languageOptions = this.languages.map((lang) => ({
				label: lang.name + " (" + lang.region + ")",
				value: lang.code,
			}));
		});

		this.settingsService.getSettings().subscribe((data: any) => {
			this.settings = data;
			this.twoFactorEnabled = data.twoFactorEnabled || false;
		});
	}

	onThemeChange: (value: string) => void = (value: string) => {
		value = value.toLowerCase();
		this.currentTheme = value;
		localStorage.setItem("Theme", value);
		this.toastrService.success(this.translateService.instant("Theme changed successfully"),this.translateService.instant("Success"));
		console.log(`Theme changed to ${value}!`);
		document.documentElement.classList.remove("dark");
		document.documentElement.classList.remove("light");
		document.documentElement.classList.add(value);

	};

	onLanguageChange: (value: string) => void = (value: string) => {
		const selectedLanguage = this.languages.find((lang) => lang.code === value);
		if (selectedLanguage) {
			this.currentLanguage = selectedLanguage.code;
			localStorage.setItem("Language", selectedLanguage.code);
			this.translateService.use(selectedLanguage.code);
			this.settingsService.updateSettings({ languageId: selectedLanguage.id }).subscribe((data) => {
				this.toastrService.success(this.translateService.instant("Language changed successfully"),this.translateService.instant("Success"));
			});
		}
	};

	onTwoFactorChange: (value: string) => void = (value: string) => {
		const enabled = value === "true";
		this.twoFactorEnabled = enabled;

		this.settingsService.updateSettings({ twoFactorEnabled: enabled }).subscribe(
			(data) => {
				this.translateService
					.get(enabled ? "Two-factor authentication enabled" : "Two-factor authentication disabled")
					.subscribe((translatedMessage: string) => {
						this.toastrService.success(translatedMessage);
					});
			},
			(error) => {
				this.translateService
					.get("Failed to update two-factor authentication settings")
					.subscribe((translatedMessage: string) => {
						this.toastrService.error(translatedMessage);
						// Revert the UI state if the API call fails
						this.twoFactorEnabled = !enabled;
					});
			},
		);
	};
}

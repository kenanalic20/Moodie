import { Component, OnInit, HostListener } from "@angular/core";
import { Router, NavigationEnd } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { AuthService } from "../services/auth.service";
import { SettingsService } from "../services/settings.service";
import { filter } from "rxjs/operators";

interface NavItem {
	label: string;
	route: string;
	icon: string;
}

@Component({
	selector: "app-header",
	templateUrl: "./header.component.html",
})
export class HeaderComponent implements OnInit {
	isMobileMenuOpen = false;
	isDarkTheme = false;
	currentLanguage = "EN";
	isLoggedIn = false;

	navigationItems: NavItem[] = [
		{ label: "Dashboard", route: "/dashboard", icon: "dashboard" },
		{ label: "Habits", route: "/habits", icon: "repeat" },
		{ label: "Goals", route: "/goals", icon: "target" },
		{ label: "Calendar", route: "/calendar", icon: "calendar" },
		{ label: "Stats", route: "/stats", icon: "chart-bar" },
		{ label: "Achievements", route: "/achievements", icon: "trophy" },
		{ label: "Settings", route: "/settings", icon: "cog" },
		{ label: "Profile", route: "/user-info", icon: "user" },
		{ label: "Export", route: "/export", icon: "download" },
	];

	constructor(
		private router: Router,
		private translate: TranslateService,
		private authService: AuthService,
		private settingsService: SettingsService,
	) {}

	ngOnInit(): void {
		// Check login status
		this.authService.isAuthenticated().subscribe((isAuthenticated) => {
			this.isLoggedIn = isAuthenticated;

			if (isAuthenticated) {
				// If logged in, get settings from backend
				this.settingsService.getSettings().subscribe({
					next: (settings) => {
						// Apply theme from backend
						if (settings.darkMode !== undefined) {
							this.isDarkTheme = settings.darkMode;
							localStorage.setItem("theme", this.isDarkTheme ? "dark" : "light");
							this.applyTheme();
						} else {
							this.loadLocalTheme();
						}

						// Apply language from backend
						if (settings.languageId !== undefined) {
							// Map languageId to language code (1 = EN, 2 = BS)
							this.currentLanguage = settings.languageId === 1 ? "EN" : "BS";
							this.translate.use(this.currentLanguage.toLowerCase());
						} else {
							this.loadLocalLanguage();
						}
					},
					error: () => {
						this.loadLocalTheme();
						this.loadLocalLanguage();
					},
				});
			} else {
				// If not logged in, just use local storage
				this.loadLocalTheme();
				this.loadLocalLanguage();
			}
		});

		// Close mobile menu on navigation
		this.router.events.pipe(filter((event) => event instanceof NavigationEnd)).subscribe(() => {
			this.closeMobileMenu();
		});
	}

	private loadLocalTheme(): void {
		this.isDarkTheme =
			localStorage.getItem("theme") === "dark" ||
			(!localStorage.getItem("theme") && window.matchMedia("(prefers-color-scheme: dark)").matches);
		this.applyTheme();
	}

	private loadLocalLanguage(): void {
		const savedLanguage = localStorage.getItem("language");
		if (savedLanguage) {
			this.currentLanguage = savedLanguage;
			this.translate.use(savedLanguage.toLowerCase());
		} else {
			this.currentLanguage = "EN";
		}
	}

	@HostListener("window:resize")
	onResize() {
		// Close mobile menu on window resize if it's open and we're on desktop
		if (this.isMobileMenuOpen && window.innerWidth >= 1024) {
			this.isMobileMenuOpen = false;
		}
	}

	@HostListener("document:keydown.escape")
	onEscapeKey() {
		// Close mobile menu when ESC key is pressed
		if (this.isMobileMenuOpen) {
			this.closeMobileMenu();
		}
	}

	toggleMobileMenu(): void {
		this.isMobileMenuOpen = !this.isMobileMenuOpen;

		// Prevent body scrolling when menu is open
		if (this.isMobileMenuOpen) {
			document.body.classList.add("overflow-hidden");
		} else {
			document.body.classList.remove("overflow-hidden");
		}
	}

	closeMobileMenu(): void {
		if (this.isMobileMenuOpen) {
			this.isMobileMenuOpen = false;
			document.body.classList.remove("overflow-hidden");
		}
	}

	toggleTheme(): void {
		this.isDarkTheme = !this.isDarkTheme;
		localStorage.setItem("theme", this.isDarkTheme ? "dark" : "light");
		this.applyTheme();

		if (this.isLoggedIn) {
			this.settingsService.updateSettings({ darkMode: this.isDarkTheme }).subscribe({
				error: (error) => console.error("Failed to save theme setting:", error),
			});
		}
	}

	applyTheme(): void {
		if (this.isDarkTheme) {
			document.documentElement.classList.add("dark");
		} else {
			document.documentElement.classList.remove("dark");
		}
	}

	toggleLanguage(): void {
		this.currentLanguage = this.currentLanguage === "EN" ? "BS" : "EN";
		localStorage.setItem("language", this.currentLanguage);
		this.translate.use(this.currentLanguage.toLowerCase());

		if (this.isLoggedIn) {
			// Map language code to languageId (EN = 1, BS = 2)
			const languageId = this.currentLanguage === "EN" ? 1 : 2;
			this.settingsService.updateSettings({ languageId: languageId }).subscribe({
				error: (error) => console.error("Failed to save language setting:", error),
			});
		}
	}

	handleAuthAction(): void {
		if (this.isLoggedIn) {
			this.logout();
		} else {
			this.router.navigate(["/login"]);
		}
	}

	logout(): void {
		this.authService.logout().subscribe({
			next: () => {
				this.router.navigate(["/login"]);
			},
		});
	}
}

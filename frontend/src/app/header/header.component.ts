import { Component, OnInit, HostListener } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from '../services/auth.service';
import { filter } from 'rxjs/operators';

interface NavItem {
    label: string;
    route: string;
    icon: string;
}

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
})
export class HeaderComponent implements OnInit {
    isMobileMenuOpen = false;
    isDarkTheme = false;
    currentLanguage = 'EN';
    isLoggedIn = false;

    navigationItems: NavItem[] = [
        { label: 'Dashboard', route: '/dashboard', icon: 'dashboard' },
        { label: 'Habits', route: '/habits', icon: 'repeat' },
        { label: 'Goals', route: '/goals', icon: 'target' },
        { label: 'Calendar', route: '/calendar', icon: 'calendar' },
        { label: 'Stats', route: '/stats', icon: 'chart-bar' },
        { label: 'Achievements', route: '/achievements', icon: 'trophy' },
        { label: 'Settings', route: '/settings', icon: 'cog' },
        { label: 'Profile', route: '/user-info', icon: 'user' },
        { label: 'Export', route: '/export', icon: 'download' },
    ];

    constructor(
        private router: Router,
        private translate: TranslateService,
        private authService: AuthService
    ) {}

    ngOnInit(): void {
        //Pleas store these values in backend for the love of GOD set defaults in the backend here!!!
        /*Combine settings from backend with local storage for example 
        if user when logged in changes language to bosnian it needs to store that id in settings table
        and when he logs out if he changes languages it shoudnt inpact the language that is stored 
        in db and when he logs in again you need to fatch the language that is stored
        on backend by id*/


        // Check if user is using dark mode
        this.isDarkTheme =
            localStorage.getItem('theme') === 'dark' ||
            (!localStorage.getItem('theme') &&
                window.matchMedia('(prefers-color-scheme: dark)').matches);

        // Apply theme
        this.applyTheme();

        // Get current language from localStorage
        const savedLanguage = localStorage.getItem('language');
        if (savedLanguage) {
            this.currentLanguage = savedLanguage;
            this.translate.use(savedLanguage.toLowerCase());
            console.log('Applied language from localStorage:', savedLanguage);
        } else {
            this.currentLanguage = 'EN';
        }

        // Check login status
        this.authService.isAuthenticated().subscribe(isAuthenticated => {
            this.isLoggedIn = isAuthenticated;
        });

        // Close mobile menu on navigation
        this.router.events
            .pipe(filter(event => event instanceof NavigationEnd))
            .subscribe(() => {
                this.closeMobileMenu();
            });
    }

    @HostListener('window:resize')
    onResize() {
        // Close mobile menu on window resize if it's open and we're on desktop
        if (this.isMobileMenuOpen && window.innerWidth >= 1024) {
            this.isMobileMenuOpen = false;
        }
    }

    @HostListener('document:keydown.escape')
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
            document.body.classList.add('overflow-hidden');
        } else {
            document.body.classList.remove('overflow-hidden');
        }
    }

    closeMobileMenu(): void {
        if (this.isMobileMenuOpen) {
            this.isMobileMenuOpen = false;
            document.body.classList.remove('overflow-hidden');
        }
    }

    toggleTheme(): void {
        this.isDarkTheme = !this.isDarkTheme;
        localStorage.setItem('theme', this.isDarkTheme ? 'dark' : 'light');
        this.applyTheme();
    }

    applyTheme(): void {
        if (this.isDarkTheme) {
            document.documentElement.classList.add('dark');
        } else {
            document.documentElement.classList.remove('dark');
        }
    }

    toggleLanguage(): void {
        this.currentLanguage = this.currentLanguage === 'EN' ? 'BS' : 'EN';
        /*Here you need to get id of a language from backend and call https request to
        settings endpoint to stor that language id on toggle. You will probably ask
        why do i need to store language id on backend and my answer is to be able to 
        translate exported files to languages chosen from thes switchers. Pleas for
        the love of GOD do it that way so we have 15 tables to be able to finish this fricking 
        subject*/
        localStorage.setItem('Language', this.currentLanguage.toLowerCase());
        console.log('Language changed to:', this.currentLanguage);
        this.translate.use(this.currentLanguage.toLowerCase());
    }

    handleAuthAction(): void {
        if (this.isLoggedIn) {
            this.logout();
        } else {
            this.router.navigate(['/login']);
        }
    }

    logout(): void {
        this.authService.logout().subscribe({
            next: () => {
                this.router.navigate(['/login']);
            },
        });
    }
}

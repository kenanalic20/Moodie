import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LanguageService } from './services/language.service';
import {
    Router,
    NavigationStart,
    NavigationEnd,
    NavigationCancel,
    NavigationError,
} from '@angular/router';
import { filter } from 'rxjs/operators';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
    title = 'Moodie';
    languageCodes: Array<string> = [];

    constructor(
        private translateService: TranslateService,
        private languageService: LanguageService,
        private router: Router
    ) {}

    ngOnInit() {
        const theme = localStorage.getItem('theme'); // Changed from 'Theme' to 'theme'
        const language = localStorage.getItem('language'); // Changed from 'Language' to 'language'

        this.languageService.getLanguage().subscribe((data: any) => {
            this.languageCodes = data.map((lang: any) => lang.code);

            // Set up languages after we get data from the server
            this.translateService.addLangs(this.languageCodes);
            this.translateService.setDefaultLang(this.languageCodes[0]);

            // Apply stored language if available
            if (language) {
                this.translateService.use(language.toLowerCase());
            } else {
                this.translateService.use(this.languageCodes[0]);
            }
        });

        if (theme) {
            document.documentElement.classList.add(theme.toLowerCase());
        }

        // Add navigation transition control
        this.router.events
            .pipe(
                filter(
                    event =>
                        event instanceof NavigationStart ||
                        event instanceof NavigationEnd ||
                        event instanceof NavigationCancel ||
                        event instanceof NavigationError
                )
            )
            .subscribe(event => {
                if (event instanceof NavigationStart) {
                    document.body.classList.add('router-navigating');
                } else {
                    setTimeout(() => {
                        document.body.classList.remove('router-navigating');
                    }, 100);
                }
            });
    }
}

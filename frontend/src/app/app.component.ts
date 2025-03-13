import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LanguageService } from './services/language.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
})
export class AppComponent {
    title = 'Moodie';
    languageCodes: Array<string> = [];

    constructor(private translateService: TranslateService,private languageService: LanguageService) {}
    
    ngOnInit() {
        const theme = localStorage.getItem('Theme');
        const language = localStorage.getItem('Language');

        this.languageService.getLanguage().subscribe((data: any) => {
            this.languageCodes = data.map((lang: any) => lang.code);
        });

        this.translateService.addLangs(this.languageCodes);
        this.translateService.setDefaultLang(this.languageCodes[0]);

        if (language) {
            this.translateService.use(language);
        } else {
            this.translateService.use(this.languageCodes[0]);
        }

        if (theme) {
            document.documentElement.classList.add(theme.toLowerCase());
        }
    }
    

}

import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
})
export class AppComponent {
    title = 'Moodie';

    constructor(private translate: TranslateService) {
        const theme = localStorage.getItem('Theme');
        const language = localStorage.getItem('Language');
        this.translate.addLangs(['bs', 'en']);
        this.translate.setDefaultLang('en');
        if (language) {
            this.translate.use(language);
        } else {
            this.translate.use('en');
        }
        if (theme) {
            document.documentElement.classList.add(theme.toLowerCase());
        }
    }
    

}

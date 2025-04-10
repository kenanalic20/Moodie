import { Component } from '@angular/core';
import { isDev } from '../../globals';
import { faCode } from '@fortawesome/free-solid-svg-icons';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-frontpage',
    templateUrl: './frontpage.component.html',
})
export class FrontpageComponent {
    isDevelopment = isDev;
    codeIcon = faCode;
    constructor(private translateService: TranslateService) {}

    switchLanguage(language: string) {
        this.translateService.use(language);
    }
}

import { Component } from '@angular/core';
import { isDev } from '../../globals';
import { faCode } from '@fortawesome/free-solid-svg-icons';

@Component({
    selector: 'app-frontpage',
    templateUrl: './frontpage.component.html',
})
export class FrontpageComponent {
    isDevelopment = isDev;
    codeIcon = faCode;
}

import { Component } from '@angular/core';
import { isDev } from '../globals';
import { faCode } from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
})
export class HeaderComponent {
  active = window.location.pathname.split('/')[1];
  isDevelopment = isDev;
  codeIcon = faCode;
}

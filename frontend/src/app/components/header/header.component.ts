import { Component } from '@angular/core';
import { isDev } from '../../globals';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
})
export class HeaderComponent {
  active = window.location.pathname.split('/')[1];
  isDevelopment = isDev;
  activeClasses =
    'hover:border-opacity-50 border-opacity-10 border-b-4 transition-all';
}

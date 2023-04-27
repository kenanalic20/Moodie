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
    'md:hover:border-opacity-50 md:border-opacity-10 md:border-b-4 transition-all';
}

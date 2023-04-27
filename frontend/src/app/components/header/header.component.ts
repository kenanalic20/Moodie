import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
})
export class HeaderComponent {
  active = 'dashboard';
  activeClasses =
    'md:hover:border-opacity-50 md:border-opacity-10 md:border-b-4';
}

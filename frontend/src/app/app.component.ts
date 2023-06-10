import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'Moodie';

  constructor() {
    const theme = localStorage.getItem('Theme');
    if (theme) {
      document.documentElement.classList.add(theme.toLowerCase());
    }
  }
}

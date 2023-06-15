import { Component } from '@angular/core';

type Display = 'simple' | 'complex';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
})
export class DashboardComponent {
    display: Display =
        (localStorage.getItem('mood-display') as Display) || 'simple';

    toggleDisplay(): void {
        this.display = this.display === 'simple' ? 'complex' : 'simple';
        localStorage.setItem('mood-display', this.display);
    }
}

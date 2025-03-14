import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

type Display = 'simple' | 'complex';

@Component({
    selector: 'app-dashboard',
    templateUrl: './dashboard.component.html',
})
export class DashboardComponent implements OnInit {
    display: Display =
        (localStorage.getItem('mood-display') as Display) || 'simple';
    selectedDate?: Date;

    constructor(private route: ActivatedRoute) {}

    ngOnInit() {
        this.route.queryParams.subscribe(params => {
            if (params['date']) {
                this.selectedDate = new Date(params['date']);
            }
        });
    }

    toggleDisplay(): void {
        this.display = this.display === 'simple' ? 'complex' : 'simple';
        localStorage.setItem('mood-display', this.display);
    }
}

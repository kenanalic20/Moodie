import { Component, ElementRef, ViewChild } from '@angular/core';
import { StatsService } from 'src/app/services/stats.service';

@Component({
    selector: 'app-stat-graph',
    templateUrl: './stat-graph.component.html',
})
export class StatGraphComponent {

    statData: any[] = [];

    constructor(
        private statService: StatsService
    ) {}
    ngOnInit() {
        this.statService.getStats().subscribe((data: any) => {
            this.statData = data
        });
    }
    
}

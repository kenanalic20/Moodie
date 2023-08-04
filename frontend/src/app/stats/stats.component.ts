import { Component } from '@angular/core';
import { StatGraphComponent } from './stat-graph/stat-graph.component';
import { StatActivitiesComponent } from './stat-activities/stat-activities.component';
import {MoodService} from "../services/mood.service";


@Component({
    selector: 'app-stats',
    templateUrl: './stats.component.html',
    entryComponents: [StatGraphComponent, StatActivitiesComponent],
})
export class StatsComponent {



}

import { Component, OnInit } from '@angular/core';
import { ActivityService } from 'src/app/services/activity.service';
import { finalize, timeout } from 'rxjs/operators';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ActivityInformationModalComponent } from '../activity-information-modal/activity-information-modal.component';

interface Activity {
  id: number;
  name: string;
}

@Component({
    selector: 'app-stat-activities',
    templateUrl: './stat-activities.component.html',
})
export class StatActivitiesComponent implements OnInit {
    constructor(
        private activityService: ActivityService,
        private modalService: BsModalService
    ) {}

    bestMoodActivities: Activity[] = [];
    worstMoodActivities: Activity[] = [];
    isLoading = true;
    hasActivities = false;
    modalRef: any;

    ngOnInit() {
        this.loadActivities();
    }

    loadActivities() {
        Promise.all([
            this.getBestMoodActivities(),
            this.getWorstMoodActivities()
        ]).then(() => {
            setTimeout(() => {
                this.isLoading = false;
                this.hasActivities = this.bestMoodActivities.length > 0 || 
                                   this.worstMoodActivities.length > 0;
            }, 1000);
        });
    }

    getBestMoodActivities() {
        return new Promise<void>((resolve) => {
            this.activityService.getBestMoodActivities()
                .pipe(
                    timeout(2000),
                    finalize(() => resolve())
                )
                .subscribe(res => {
                    this.bestMoodActivities = Object.values(res);
                });
        });
    }

    getWorstMoodActivities() {
        return new Promise<void>((resolve) => {
            this.activityService.getWorstMoodActivities()
                .pipe(
                    timeout(2000),
                    finalize(() => resolve())
                )
                .subscribe(res => {
                    this.worstMoodActivities = Object.values(res);
                });
        });
    }

    openModal(activity?:any) {
        this.modalRef = this.modalService.show(ActivityInformationModalComponent);
        this.modalRef.content.activity = activity;
    }
}

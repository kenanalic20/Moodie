import { Component, OnInit } from '@angular/core';
import { ActivityService } from 'src/app/services/activity.service';
import { finalize, timeout } from 'rxjs/operators';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ActivityInformationModalComponent } from '../activity-information-modal/activity-information-modal.component';
import { ActivityEditModalComponent } from '../activity-edit-modal/activity-edit-modal.component';
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
    hasBestActivities = false;
    hasWorstActivities = false;
    modalRef: any;

    ngOnInit(): void {
        this.loadActivities();
    }

    loadActivities() {
        this.bestMoodActivities = [];
        this.worstMoodActivities = [];
        this.isLoading = true;

        Promise.all([
            this.getBestMoodActivities(),
            this.getWorstMoodActivities(),
        ]).then(() => {
            this.isLoading = false;
            this.updateActivityStates();
        });
    }

    private updateActivityStates() {
        this.hasBestActivities = this.bestMoodActivities.length > 0;
        this.hasWorstActivities = this.worstMoodActivities.length > 0;
    }

    getBestMoodActivities() {
        return new Promise<void>(resolve => {
            this.activityService
                .getBestMoodActivities()
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
        return new Promise<void>(resolve => {
            this.activityService
                .getWorstMoodActivities()
                .pipe(
                    timeout(2000),
                    finalize(() => resolve())
                )
                .subscribe(res => {
                    this.worstMoodActivities = Object.values(res);
                });
        });
    }

    reloadActivities() {
        this.isLoading = true;
        this.loadActivities();
        this.modalRef?.hide();
    }

    openModal(activity?: any) {
        this.modalRef = this.modalService.show(
            ActivityInformationModalComponent,
            {
                initialState: {
                    activity: activity,
                },
            }
        );

        if (this.modalRef.content) {
            const modalComponent = this.modalRef
                .content as ActivityInformationModalComponent;
            modalComponent.activityDeleted.subscribe({
                next: () => {
                    this.reloadActivities();
                },
            });

            modalComponent.activityUpdated.subscribe({
                next: () => {
                    this.reloadActivities();
                },
            });
        }
    }
}

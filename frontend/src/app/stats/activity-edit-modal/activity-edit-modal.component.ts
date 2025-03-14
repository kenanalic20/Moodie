import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ActivityService } from 'src/app/services/activity.service';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-activity-edit-modal',
    templateUrl: './activity-edit-modal.component.html',
    imports: [FormsModule, TranslateModule],
    standalone: true,
})
export class ActivityEditModalComponent implements OnInit {
    constructor(
        public bsModalRef: BsModalRef,
        private toastrService: ToastrService,
        private activityService: ActivityService,
        private translateService: TranslateService
    ) {}

    @Input() activity: any;
    @Output() onClose = new EventEmitter<void>();

    activityName: string = '';
    activityDescription: string = '';

    ngOnInit() {
        this.activityName = this.activity.name;
        this.activityDescription = this.activity.description;
    }

    closeModal() {
        this.bsModalRef.hide();
    }

    saveActivity() {
        if (this.activity && this.activity.id) {
            this.activityService
                .updateActivity(
                    this.activity.moodId,
                    this.activity.id,
                    this.activityName,
                    this.activityDescription
                )
                .subscribe({
                    next: () => {
                        this.toastrService.success(
                            this.translateService.instant(
                                'Activity updated successfully'
                            ),
                            'Success'
                        );
                        this.onClose.emit();
                        this.bsModalRef.hide();
                    },
                    error: () => {
                        this.toastrService.error(
                            this.translateService.instant(
                                'Error updating activity'
                            ),
                            'Error'
                        );
                    },
                });
        }
    }
}

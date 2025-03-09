import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ActivityService } from 'src/app/services/activity.service';
import { TranslateService } from '@ngx-translate/core';
import { ConfirmationDialogComponent } from 'src/app/confirmation-dialog/confirmation-dialog.component';
import { ActivityEditModalComponent } from '../activity-edit-modal/activity-edit-modal.component';
@Component({
  selector: 'app-activity-information-modal',
  templateUrl: './activity-information-modal.component.html',
})
export class ActivityInformationModalComponent {
  constructor(
    public bsModalRef: BsModalRef,
    public bsModalService:BsModalService,
    private toastrService: ToastrService,
    private activityService: ActivityService,
    private translateService: TranslateService
  ) { }

  @Input() activity: any = {};
  @Output() activityDeleted = new EventEmitter<void>();
  @Output() activityUpdated = new EventEmitter<void>();

  formatDate(dateString: string): string {
    const date = new Date(dateString);
    const day = date.getDate();
    const month = date.getMonth() + 1;
    const year = date.getFullYear();
    return `${day}/${month}/${year}`;
  }
  
  closeModal() {
    console.log(this.activity)
    this.bsModalRef.hide();
  }

  editActivity(activity: any) {
    const initialState = {
      activity,
      activityUpdated: new EventEmitter<void>()
    };
    const modalRef=this.bsModalService.show(ActivityEditModalComponent, {initialState});
    if(modalRef.content){
      modalRef.content.onClose.subscribe(()=>{
        this.activityUpdated.emit();
        this.bsModalRef.hide();
      })
    }
  }



  removeActivity(id: number) {
    const modalRef = this.bsModalService.show(ConfirmationDialogComponent, {
      initialState: {
        title: this.translateService.instant('Delete Item'),
        message: this.translateService.instant('Are you sure you want to delete this item?')
      }
    });
    if (modalRef.content) {
      modalRef.content.onClose.subscribe((result: boolean) => {
        if (result) {
          this.activityService.deleteActivity(id).subscribe({
            next: () => {
              this.toastrService.success(
                this.translateService.instant('Activity deleted successfully'),'Success'
              );
              this.activityDeleted.emit();
              this.bsModalRef.hide();
            },
            error: (error) => {
              this.toastrService.error(
                this.translateService.instant('An error occurred while deleting the activity'),'Error'
              );
            }
          });
        }
        modalRef.hide();
      });
    }
  }
}

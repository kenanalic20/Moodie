import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { ActivityService } from 'src/app/services/activity.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-activity-information-modal',
  templateUrl: './activity-information-modal.component.html',
})
export class ActivityInformationModalComponent {
  constructor(
    public bsModalRef: BsModalRef,
    private toastrService: ToastrService,
    private activityService: ActivityService,
    private translateService: TranslateService
  ) { }

  @Input() activity: any = {};

  ngOnInit(): void {
  }
  
  closeModal() {
    console.log(this.activity)
    this.bsModalRef.hide();
  }

  updateActivity() {

  }

  removeActivity() {
    
  }
 
}

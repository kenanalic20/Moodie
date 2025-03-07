import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { NotesService } from "../../services/notes.service";
import { ActivityService } from 'src/app/services/activity.service';
import { ToastrService } from "ngx-toastr";
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-mood-information-modal',
  templateUrl: './mood-information-modal.component.html',
})
export class MoodInformationModalComponent implements OnDestroy, OnInit {
  constructor(
    public bsModalRef: BsModalRef,
    private notesService: NotesService,
    private toastrService: ToastrService,
    private activityService: ActivityService,
    private translateService: TranslateService
  ) { }

  @Input() mood = 0;
  @Input() moodId = 0;
  activityInput: boolean = false;
  selectedImage: File | null = null;
  title: string = '';
  description: string = '';
  activityName?: string;
  activityDescription?: string;

  activities: any = [];

  imageUrl: string | null = null;
  activity: any;
  selectedActivities: Set<number> = new Set();

  onImageSelected(event: Event) {
    const inputElement = event.target as HTMLInputElement;
    if (inputElement.files && inputElement.files.length > 0) {
      this.selectedImage = inputElement.files[0];
      this.updateImageSrc();
    }
  }

  updateImageSrc(): void {
    if (this.selectedImage) {
      this.imageUrl = URL.createObjectURL(this.selectedImage);
    } else {
      this.imageUrl = null;
    }
  }

  closeModal() {
    this.bsModalRef.hide();
  }
  
  saveActivity() {
    this.activityService.addActivity(this.activityName, this.activityDescription).subscribe(res => {
      this.toastrService.success('Activity added successfully', 'Success')
      this.showActivityInput();
      this.activityService.getActivitiesByUserId().subscribe(res => {
        this.activities = res;
      });
    },
    error=>{
      console.log(error)
      //Translated notification
      this.translateService.get(error.error).subscribe((res: string) => {
        this.toastrService.error(res, this.translateService.instant('Error'));
      });
    },
  );
    
  }

  setNotes() {
    this.notesService.addNotes(this.title, this.selectedImage, this.description).subscribe(res => {
      this.toastrService.success('Notes added successfuly', 'Success');
    },
    error=>{
      this.translateService.get(error.error).subscribe((res: string) => {
        this.toastrService.error(res,this.translateService.instant('Error'));
      });
      }
    );

    this.title = '';
    this.description = '';
    this.selectedImage = null;
    this.imageUrl = null;

  }

  showActivityInput() {
    this.activityInput = !this.activityInput;
  }

  ngOnDestroy(): void {
    if (this.imageUrl) {
      URL.revokeObjectURL(this.imageUrl);
    }
  }

  getActivities() {
    this.activityService.getActivitiesByUserId().subscribe(res => {
      this.activities = res;
    });
  }

  selectedActivity(id: number) {
    this.activityService.updateActivity(this.moodId,id).subscribe(res => {
      this.selectedActivities.add(id);
      
      this.toastrService.success(this.translateService.instant('Activity selected successfuly?'),this.translateService.instant('Error'));
      
    });
  }

  unselectActivity(id: number) {
    this.activityService.updateActivity(undefined, id).subscribe(res => {
      this.selectedActivities.delete(id);
    });
  }

  ngOnInit(): void {
    this.getActivities();
  }

}

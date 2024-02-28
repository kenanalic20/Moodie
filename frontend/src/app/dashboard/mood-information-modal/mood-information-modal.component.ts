import {Component, Input, OnDestroy, OnInit} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import {NotesService} from "../../services/notes.service";
import { ActivityService } from 'src/app/services/activity.service';
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-mood-information-modal',
  templateUrl: './mood-information-modal.component.html',
})
export class MoodInformationModalComponent implements OnDestroy,OnInit {
  constructor(
    public bsModalRef: BsModalRef,
    private notesService: NotesService,
    private toastr: ToastrService,
    private activityService:ActivityService
  ) {}

  @Input() mood = 0;
  activityInput:boolean=false;
  selectedImage: File | null = null;
  title:string = '';
  description:string = '';
  activityTitle:string='';
  activityDescription:string='';

  activities:any=[];

  imageUrl: string | null = null;
    activity: any;

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
 
  CloseModal() {
    this.bsModalRef.hide();
  }
  saveActivity(){
    if(this.activityTitle==''){
      this.toastr.error("Add title to activity","Failure");
    }
    else{
      this.activityService.addActivity(this.activityTitle,this.activityDescription).subscribe(res=>{
       this.toastr.success('Activity added successfully', 'Success')
       this.showActivityInput();
       this.activityService.getActivitiesByUserId().subscribe(res=>{
        this.activities=res;
      });

      });
    }
  }
  setNotes() {
    this.notesService.addNotes(this.title, this.selectedImage, this.description).subscribe((data: any) => {
      console.log(data);
    });

    this.title = '';
    this.description = '';
    this.selectedImage = null;
    this.imageUrl = null;

    this.toastr.success('Note added successfully', 'Success');
  }
 showActivityInput(){
  this.activityInput=!this.activityInput;
 }
  ngOnDestroy(): void {
    if (this.imageUrl) {
      URL.revokeObjectURL(this.imageUrl);
    }
  }
 ngOnInit(): void {
    this.activityService.getActivitiesByUserId().subscribe(res=>{
      this.activities=res;
    });
 }

}

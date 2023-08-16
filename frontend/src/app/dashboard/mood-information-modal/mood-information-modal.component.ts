import { Component, Input } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import {NotesService} from "../../services/notes.service";


@Component({
    selector: 'app-mood-information-modal',
    templateUrl: './mood-information-modal.component.html',
})
export class MoodInformationModalComponent {
    constructor(public bsModalRef: BsModalRef ,private notesService:NotesService) {}

    @Input() mood = 0;
  selectedImage:File|null=null ;
  title = '';
  description = '';

  onImageSelected(event: any) {
    this.selectedImage = event.target.files[0];
  }

    CloseModal() {
        this.bsModalRef.hide();
    }
    //get values from input and text area and file
   setNotes() {

     this.notesService.addNotes(this.title,this.selectedImage,this.description).subscribe((data:any)=>{
       console.log(data);
     });

     alert ('Notes Added Successfully');
   }




}

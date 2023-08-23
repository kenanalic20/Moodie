import {Component, Input, OnDestroy} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import {NotesService} from "../../services/notes.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-mood-information-modal',
  templateUrl: './mood-information-modal.component.html',
})
export class MoodInformationModalComponent implements OnDestroy {
  constructor(
    public bsModalRef: BsModalRef,
    private notesService: NotesService,
    private toastr: ToastrService
  ) {}

  @Input() mood = 0;
  selectedImage: File | null = null;
  title = '';
  description = '';
  imageUrl: string | null = null;

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

  ngOnDestroy(): void {
    if (this.imageUrl) {
      URL.revokeObjectURL(this.imageUrl);
    }
  }
}

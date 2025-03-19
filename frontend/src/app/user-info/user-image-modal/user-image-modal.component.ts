import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { UserImageService } from '../../services/user-image.service';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-user-image-modal',
    templateUrl: './user-image-modal.component.html',
})
export class UserImageModalComponent {
    selectedImage: File | null = null;
    imageUrl: string | null = null;
    imageStatus: string = '';
    isLoading: boolean = false;

    @Output() imageUploaded = new EventEmitter<string>();
    @Output() status = new EventEmitter<string>();
    @Output() date = new EventEmitter<string>();

    constructor(
        public bsModalRef: BsModalRef,
        private userImageService: UserImageService,
        private toastrService: ToastrService,
        private translateService: TranslateService
    ) {}

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
        }
    }

    formatDate(dateString: Date): string {
        const date = new Date(dateString);
        const day = date.getDate(); 
        const month = date.getMonth() + 1; 
        const year = date.getFullYear();
        return `${day}/${month}/${year}`;
    }

    closeModal() {
        this.bsModalRef.hide();
    }

    saveImage() {
        if (!this.selectedImage) {
            this.toastrService.error(
                this.translateService.instant('Please select an image'),
                this.translateService.instant('Error')
            );
            return;
        }

        this.isLoading = true;

        const formData = new FormData();
        formData.append('Status', this.imageStatus);
        formData.append('Image', this.selectedImage);
        this.userImageService.addUserImage(formData).subscribe(
            (response: any) => {
                this.toastrService.success(
                    this.translateService.instant('Image uploaded successfully'),
                    this.translateService.instant('Success')
                );
                this.isLoading = false;
                this.imageUploaded.emit(response.imagePath); // Emit the new image URL
                this.status.emit(response.status);
                this.date.emit(this.formatDate(response.date));
                this.closeModal();
            },
            error => {
                this.toastrService.error(
                    this.translateService.instant('Failed to upload image'),
                    this.translateService.instant('Error')
                );
                this.isLoading = false;
            }
        );
    }

}
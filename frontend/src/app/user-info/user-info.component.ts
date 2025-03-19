import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserInfo, UserInfoService } from '../services/user-info.service';
import { TranslateService } from '@ngx-translate/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UserImageModalComponent } from './user-image-modal/user-image-modal.component';
import { UserImageService } from '../services/user-image.service';
import { UserLocationService } from '../services/user-location.service';
import { AuthService } from '../services/auth.service';
@Component({
    selector: 'app-user-info',
    templateUrl: './user-info.component.html',
})
export class UserInfoComponent implements OnInit {
    userInfoForm: FormGroup;
    userInfo: UserInfo = {};
    isLoading = false;
    isSaving = false;
    saveSuccess = false;
    saveError = false;
    profilePhotoPreview: string | null = null;
    status: string = '';
    date: string = '';
    userName:string = '';

    constructor(
        private userInfoService: UserInfoService,
        private fb: FormBuilder,
        private translateService: TranslateService,
        private modalService: BsModalService,
        private bsModalRef: BsModalRef,
        private userImageService: UserImageService,
        private userLocationService: UserLocationService,
        private authService: AuthService
    ) {
        this.userInfoForm = this.fb.group({
            firstName: [''],
            lastName: [''],
            gender: [''],
            birthday: [''],
            country: [''],
            province: [''],
            city: ['']
        });
    }

    ngOnInit(): void {
        this.loadUserInfo();
        this.loadProfilePhoto();
        this.loadUserLocation();
        this.loadUserName();
    }

    formatDate(dateString: Date): string {
        const date = new Date(dateString);
        const day = date.getDate();
        const month = date.getMonth() + 1;
        const year = date.getFullYear();
        return `${day}/${month}/${year}`;
    }

    loadUserName(): void {
        this.authService.user().subscribe({
            next: (user: any) => {
                this.userName = user.username;
            }
        });
    }

    loadProfilePhoto(): void {
        this.userImageService.getUserImage().subscribe(
            (response: any) => {
                if (response.imagePath) {
                    this.profilePhotoPreview = response.imagePath;
                    this.userInfoForm.patchValue({
                        profilePhoto: response.imagePath,
                    });
                }
                this.status = response.status;
                this.date = this.formatDate(response.date);
            });
    }

    loadUserLocation(): void {
        this.userLocationService.getUserLocation().subscribe({
            next: (locationInfo: any) => {
                this.userInfoForm.patchValue({
                    country: locationInfo.country || '',
                    province: locationInfo.province || '',
                    city: locationInfo.city || '',
                });
            },
        });
    }

    loadUserInfo(): void {
        this.isLoading = true;
        this.userInfoService.getUserInfo().subscribe({
            next: info => {
                if (info) {
                    this.userInfo = info;
                } else {
                    this.userInfo = {
                        firstName: '',
                        lastName: '',
                        gender: '',
                        birthday: undefined,
                    };
                }
                this.patchFormValues();
                this.isLoading = false;
            },
            error: () => {
                this.userInfo = {};
                this.patchFormValues();
                this.isLoading = false;
            },
            complete: () => {
                this.isLoading = false;
            },
        });
    }

    openImageModal() {

        this.bsModalRef = this.modalService.show(UserImageModalComponent);

        this.bsModalRef.content.imageUploaded.subscribe((imageUrl: string) => {
            this.profilePhotoPreview = imageUrl;
            this.userInfoForm.patchValue({
                profilePhoto: imageUrl,
            });
        });

        this.bsModalRef.content.status.subscribe((status: string) => {
            this.status = status;
        });

        this.bsModalRef.content.date.subscribe((date: string) => {
            this.date = date;
        });
    }

    patchFormValues(): void {
        let formattedBirthday = this.userInfo.birthday;
        if (formattedBirthday) {
            formattedBirthday = new Date(formattedBirthday);
        }

        this.userInfoForm.patchValue({
            firstName: this.userInfo.firstName || '',
            lastName: this.userInfo.lastName || '',
            gender: this.userInfo.gender || '',
            birthday: formattedBirthday
                ? formattedBirthday.toISOString().split('T')[0]
                : '',
        });
    }

    removeProfilePhoto(): void {
        if (confirm(this.translateService.instant('Are you sure you want to remove your profile photo and status?'))) {
            this.userImageService.deleteUserImage().subscribe(
                (response: any) => {
                    this.status = '';
                    this.date = '';
                    this.profilePhotoPreview = null;
                    this.saveSuccess = true;
                });
        }
    }

    saveUserInfo(): void {
        this.isSaving = true;
        this.saveSuccess = false;
        this.saveError = false;
    
        // Save user information using UserInfoService
        const updatedInfo: UserInfo = {
            firstName: this.userInfoForm.value.firstName,
            lastName: this.userInfoForm.value.lastName,
            gender: this.userInfoForm.value.gender,
            birthday: this.userInfoForm.value.birthday
                ? new Date(this.userInfoForm.value.birthday)
                : undefined,
        };
    
        this.userInfoService.updateUserInfo(updatedInfo).subscribe({
            next: () => {
                // Save location information using UserLocationService
                const locationInfo = {
                    country: this.userInfoForm.value.country,
                    province: this.userInfoForm.value.province,
                    city: this.userInfoForm.value.city,
                };
    
                this.userLocationService.addUserLocation(locationInfo).subscribe({
                    next: () => {
                        this.isSaving = false;
                        this.saveSuccess = true;
                        this.loadUserInfo(); // Reload user info
                    },
                    error: () => {
                        this.isSaving = false;
                        this.saveError = true;
                    },
                });
            },
            error: () => {
                this.isSaving = false;
                this.saveError = true;
            },
        });
    }

    deleteUserInfo(): void {
        if (
            confirm(
                this.translateService.instant('Are you sure you want to delete your personal information?')
            )
        ) {
            this.userInfoService.deleteUserInfo().subscribe(res => {
                this.userInfo = {};
                this.userInfoForm.reset();
                this.saveSuccess = true;
            }
            );
            this.userLocationService.deleteUserLocation().subscribe(res => {
                this.userInfo = {};
                this.userInfoForm.reset();
                this.saveSuccess = true;
            });
        }
       
    }
}
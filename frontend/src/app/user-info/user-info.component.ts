import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { UserInfo, UserInfoService } from '../services/user-info.service';

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

    constructor(
        private userInfoService: UserInfoService,
        private fb: FormBuilder
    ) {
        this.userInfoForm = this.fb.group({
            firstName: [''],
            lastName: [''],
            gender: [''],
            birthday: [''],
            profilePhoto: [''],
        });
    }

    ngOnInit(): void {
        this.loadUserInfo();
    }

    loadUserInfo(): void {
        this.isLoading = true;
        this.userInfoService.getUserInfo().subscribe({
            next: info => {
                // Handle case where info might be empty (204 response)
                if (info) {
                    this.userInfo = info;
                    this.profilePhotoPreview = info.profilePhoto || null;
                } else {
                    // Initialize empty user info if response is empty
                    this.userInfo = {
                        firstName: '',
                        lastName: '',
                        gender: '',
                        birthday: undefined,
                        profilePhoto: '',
                    };
                }
                this.patchFormValues();
                this.isLoading = false;
            },
            error: () => {
                // On error, still stop loading and initialize form with empty values
                this.userInfo = {};
                this.patchFormValues();
                this.isLoading = false;
            },
            complete: () => {
                // Ensure isLoading is set to false on complete
                this.isLoading = false;
            },
        });
    }

    patchFormValues(): void {
        // Format the birthday if it exists
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
            profilePhoto: this.userInfo.profilePhoto || '',
        });
    }

    onFileSelected(event: Event): void {
        const input = event.target as HTMLInputElement;
        if (input.files && input.files.length > 0) {
            const file = input.files[0];

            // Check file size (limit to 2MB)
            if (file.size > 2 * 1024 * 1024) {
                alert('File size should not exceed 2MB');
                return;
            }

            const reader = new FileReader();
            reader.onload = () => {
                // Store base64 string in both the preview and form control
                const base64String = reader.result as string;
                this.profilePhotoPreview = base64String;
                this.userInfoForm.patchValue({
                    profilePhoto: base64String,
                });
            };
            reader.readAsDataURL(file); // This converts the file to a base64 data URL
        }
    }

    removeProfilePhoto(): void {
        this.profilePhotoPreview = null;
        this.userInfoForm.patchValue({
            profilePhoto: null,
        });
    }

    saveUserInfo(): void {
        this.isSaving = true;
        this.saveSuccess = false;
        this.saveError = false;

        const updatedInfo: UserInfo = {
            firstName: this.userInfoForm.value.firstName,
            lastName: this.userInfoForm.value.lastName,
            gender: this.userInfoForm.value.gender,
            birthday: this.userInfoForm.value.birthday
                ? new Date(this.userInfoForm.value.birthday)
                : undefined,
            profilePhoto: this.userInfoForm.value.profilePhoto, // This contains the base64 string
        };

        console.log(
            'Saving profile photo as base64:',
            updatedInfo.profilePhoto
                ? `${updatedInfo.profilePhoto.substring(0, 50)}... (truncated)`
                : 'No photo'
        );

        this.userInfoService.updateUserInfo(updatedInfo).subscribe({
            next: () => {
                this.isSaving = false;
                this.saveSuccess = true;
                this.loadUserInfo(); // Reload to get latest data
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
                'Are you sure you want to delete your personal information?'
            )
        ) {
            this.userInfoService.deleteUserInfo().subscribe({
                next: () => {
                    this.userInfo = {};
                    this.profilePhotoPreview = null;
                    this.userInfoForm.reset();
                    this.saveSuccess = true;
                },
                error: () => {
                    this.saveError = true;
                },
            });
        }
    }
}

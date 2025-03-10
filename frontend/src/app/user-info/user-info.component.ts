import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup } from "@angular/forms";
import { UserInfo, UserInfoService } from "../services/user-info.service";

@Component({
	selector: "app-user-info",
	templateUrl: "./user-info.component.html",
})
export class UserInfoComponent implements OnInit {
	userInfoForm: FormGroup;
	userInfo: UserInfo = {};
	isLoading = false;
	isSaving = false;
	saveSuccess = false;
	saveError = false;

	constructor(
		private userInfoService: UserInfoService,
		private fb: FormBuilder,
	) {
		this.userInfoForm = this.fb.group({
			firstName: [""],
			lastName: [""],
			gender: [""],
			birthday: [""],
		});
	}

	ngOnInit(): void {
		this.loadUserInfo();
	}

	loadUserInfo(): void {
		this.isLoading = true;
		this.userInfoService.getUserInfo().subscribe({
			next: (info) => {
				// Handle case where info might be empty (204 response)
				if (info) {
					this.userInfo = info;
				} else {
					// Initialize empty user info if response is empty
					this.userInfo = {
						firstName: "",
						lastName: "",
						gender: "",
						birthday: undefined,
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
			firstName: this.userInfo.firstName || "",
			lastName: this.userInfo.lastName || "",
			gender: this.userInfo.gender || "",
			birthday: formattedBirthday ? formattedBirthday.toISOString().split("T")[0] : "",
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
			birthday: this.userInfoForm.value.birthday ? new Date(this.userInfoForm.value.birthday) : undefined,
		};

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
		if (confirm("Are you sure you want to delete your personal information?")) {
			this.userInfoService.deleteUserInfo().subscribe({
				next: () => {
					this.userInfo = {};
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

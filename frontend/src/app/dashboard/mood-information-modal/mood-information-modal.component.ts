import { Component, EventEmitter, Input, OnDestroy, OnInit, Output } from "@angular/core";
import { BsModalRef } from "ngx-bootstrap/modal";
import { NotesService } from "../../services/notes.service";
import { ActivityService } from "src/app/services/activity.service";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";
import { MoodActivityService } from "src/app/services/mood-activity.service";
import { MoodService } from "src/app/services/mood.service";

@Component({
	selector: "app-mood-information-modal",
	templateUrl: "./mood-information-modal.component.html",
})
export class MoodInformationModalComponent implements OnDestroy, OnInit {
	constructor(
		public bsModalRef: BsModalRef,
		private notesService: NotesService,
		private toastrService: ToastrService,
		private activityService: ActivityService,
		private translateService: TranslateService,
		private moodActivityService: MoodActivityService,
		private moodService: MoodService
	) {}

	@Input() mood = 0;
	@Input() moodId = 0;
	@Input() notesId = 0;
	@Output() onNotesEdit = new EventEmitter<any>();
	activityInput: boolean = false;
	selectedImage: File | null = null;
	title: string = "";
	description: string = "";
	activityName?: string;
	activityDescription?: string;
	activities: any = [];
	imageUrl: string | null = null;
	selectedActivities: Set<number> = new Set();
	isLoading: boolean = false; 
	response:any;

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
		this.activityService.addActivity(this.activityName, this.activityDescription).subscribe(
			(res) => {
				this.toastrService.success("Activity added successfully", this.translateService.instant("Success"));
				this.showActivityInput();
				this.activityService.getActivitiesByUserId().subscribe((res) => {
					this.activities = res;
				});
			},
			(error) => {
				this.toastrService.error(this.translateService.instant(error.error), this.translateService.instant("Error"));
			},
		);
	}

	setNotes() {
		if (!this.title || !this.description) {
			this.toastrService.error("Title and Description are required", this.translateService.instant("Error"));
			return;
		}
	
		this.isLoading = true;
	
		const formData = new FormData();
		if(this.notesId != 0) {
			formData.append("Id", this.notesId.toString());
		}
		formData.append("Title", this.title);
		formData.append("Description", this.description);
	
		if (this.moodId !== null && this.moodId !== undefined) {
			formData.append("MoodId", this.moodId.toString());
		}
	
		if (this.selectedImage) {
			formData.append("Image", this.selectedImage, this.selectedImage.name);
		}
		if(this.notesId == 0) {
			this.notesService.addNotes(formData).subscribe(
				() => {
					this.toastrService.success("Notes added successfully", this.translateService.instant("Success"));
					this.isLoading = false;
					this.closeModal();
				}
			);
		}
		else {
			this.notesService.updateNotes(formData).subscribe((res:any)=>{
				this.toastrService.success(res.message, this.translateService.instant("Success"));
				this.bsModalRef.hide()
				this.moodService.getMoods().subscribe(updatedMoods => {
					this.onNotesEdit.emit(updatedMoods);
				});
			})
		}
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
		this.activityService.getActivitiesByUserId().subscribe((res) => {
			this.activities = res;
		});
	}

	selectedActivity(id: number) {
		this.moodActivityService.addMoodActivity(this.moodId,id).subscribe((res) => {
			this.selectedActivities.add(id);
			this.toastrService.success("Activity selected successfully", this.translateService.instant("Success"));
		});

	}

	unselectActivity(id: number) {
		this.moodActivityService.deleteMoodActivity(this.moodId,id).subscribe((res) => {
			this.selectedActivities.delete(id);
			this.toastrService.success("Activity selected successfully", this.translateService.instant("Success"));
		});
	}

	loadInputs() {
		this.notesService.getNoteById(this.notesId).subscribe(
			(note:any) => {
				this.title = note.title;
				this.description = note.description;
				if (note.imagePath) {
					this.imageUrl = note.imagePath;
				}
			},
			(error) => {
				this.translateService.get(error.error).subscribe((res: string) => {
					this.toastrService.error(res, this.translateService.instant("Error"));
				});
			}
		);
	}

	ngOnInit() {
		this.getActivities();
		if(this.notesId!=0) {
			this.loadInputs();
		}
	}
}
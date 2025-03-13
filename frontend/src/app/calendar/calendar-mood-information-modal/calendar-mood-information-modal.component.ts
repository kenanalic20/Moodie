import { Component, Input } from "@angular/core";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { Mood } from "../../types";
import { NotesService } from "src/app/services/notes.service";
import { ToastrService } from "ngx-toastr";
import { MoodService } from "src/app/services/mood.service";
import { EventEmitter, Output } from "@angular/core";
import { MoodInformationModalComponent } from "src/app/dashboard/mood-information-modal/mood-information-modal.component";
import { TranslateService } from "@ngx-translate/core";

@Component({
	selector: "app-calendar-mood-information-modal",
	templateUrl: "./calendar-mood-information-modal.component.html",
})
export class CalendarMoodInformationModalComponent {
	constructor(public bsModalRef: BsModalRef, 
		private notesService:NotesService,
		private toastrService:ToastrService,
		private moodService:MoodService,
		private modalService: BsModalService,
		private translateService:TranslateService
		
	) {}

	@Input() moods: Mood[] = [];
	@Input() moodCount: number = 0;
	@Output() moodDeleted = new EventEmitter<number>();
	
	response?:any;

	getMoodName(value: number): string {
		const moodMap: { [key: number]: string } = {
			1: "Awful",
			2: "Bad",
			3: "Okay",
			4: "Good",
			5: "Great",
		};
		return moodMap[value] || "Unknown";
	}

	getMoodIcon(value: number): string {
		const moodIcons: { [key: number]: string } = {
			1: "😢",
			2: "😕",
			3: "😐",
			4: "🙂",
			5: "😄",
		};
		return moodIcons[value] || "❓";
	}

	formatDate(date: Date): string {
		return new Date(date).toLocaleString("en-US", {
			hour: "numeric",
			minute: "numeric",
			hour12: true,
		});
	}

	getMoodNotes(mood: Mood): any[] {
		return mood.notes || [];
	}

	getMoodActivities(mood: Mood): any[] {
		return mood.moodActivities || [];
	}

	hasNotes(mood: Mood): boolean {
		return (mood.notes?.length ?? 0) > 0;
	}

	hasActivities(mood:Mood): boolean {
		return (mood.moodActivities?.length ?? 0) > 0;
	}

	CloseModal() {
		this.bsModalRef.hide();
	}

	removeNotes(id:number) {
		if(confirm("This action will remove both notes and mood!!")){
			this.notesService.deleteNotes(id).subscribe((res:any) => {
				this.moods = this.moods.map((mood) => {
					console.log(mood);
					if (mood.notes) {
						mood.notes = mood.notes.filter((note) => note.id !== id); 
					}
					return mood;
				});
				this.toastrService.success(this.translateService.instant("Notes removed successfully"), this.translateService.instant("Success"));

			});
		}
	}

	removeMood(mood: any) {
		this.moodService.deleteMood(mood.id).subscribe(
			(res:any) => {
				this.toastrService.success("Mood removed successfully", "Success");

				this.moods = this.moods.filter((m) => m.id !== mood.id);

				this.moodDeleted.emit(mood.id);
			}
		);
	}

	ngOnChange() {
		
	}

	editNotes(notesId:number, mood:Mood) {
		const initialState = {
			notesId,
			mood: mood.moodValue,
			moodId: mood.id
		};
		
		const modalref = this.modalService.show(MoodInformationModalComponent,{initialState});
		if(modalref.content) {
			modalref.content.onNotesEdit.subscribe((moods)=>{
				this.moods	= moods
			})
		}
	}

}

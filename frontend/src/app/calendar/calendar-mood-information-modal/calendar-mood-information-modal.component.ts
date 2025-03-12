import { Component, Input } from "@angular/core";
import { BsModalRef } from "ngx-bootstrap/modal";
import { Mood } from "../../types";
import { NotesService } from "src/app/services/notes.service";
import { ToastrService } from "ngx-toastr";

@Component({
	selector: "app-calendar-mood-information-modal",
	templateUrl: "./calendar-mood-information-modal.component.html",
})
export class CalendarMoodInformationModalComponent {
	constructor(public bsModalRef: BsModalRef, 
		private notesService:NotesService,
		private toastrService:ToastrService
	) {}

	@Input() moods: Mood[] = [];
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

	CloseModal() {
		this.bsModalRef.hide();
	}

	removeNotes(id:number) {
		console.log(id)
		if(confirm("This action will remove both notes and mood!!")){
			this.notesService.deleteNotes(id).subscribe(res => {
				this.response = res;
				this.moods = this.moods.map((mood) => {
					if (mood.notes) {
					  mood.notes = mood.notes.filter((note) => note.id !== id);
					}
					return mood;
				});
				this.toastrService.success(this.response,"Success")
			});
		}
	}

	removeMood(id:number) {
		
	}

	editNotes(id:number) {
		console.log(id)
	}

	//     Close modal on click outside
}

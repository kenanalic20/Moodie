import { Component, Input } from "@angular/core";
import { BsModalRef } from "ngx-bootstrap/modal";
import { Mood } from "../../types";

@Component({
	selector: "app-calendar-mood-information-modal",
	templateUrl: "./calendar-mood-information-modal.component.html",
})
export class CalendarMoodInformationModalComponent {
	constructor(public bsModalRef: BsModalRef) {}

	@Input() moods: Mood[] = [];

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

	hasNotes(mood: Mood): boolean {
		return (mood.notes?.length ?? 0) > 0;
	}

	CloseModal() {
		this.bsModalRef.hide();
	}

	//     Close modal on click outside
}

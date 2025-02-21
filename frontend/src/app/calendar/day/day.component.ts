import { Component, Input } from "@angular/core";
import { BsModalService } from "ngx-bootstrap/modal";
import { Router } from "@angular/router";
import { Day } from "../../types";
import { CalendarMoodInformationModalComponent } from "../calendar-mood-information-modal/calendar-mood-information-modal.component";

type MoodValue = 1 | 2 | 3 | 4 | 5;
type MoodEmojis = { [K in MoodValue]: string };

@Component({
	selector: "app-day",
	templateUrl: "./day.component.html",
})
export class DayComponent {
	constructor(
		private modalService: BsModalService,
		private router: Router,
	) {}

	@Input() day: Day = { dayOfMonth: 0, dayName: "", isLast: false };

	emojis = {
		noMood: "👀",
		mood: {
			1: "😭",
			2: "😔",
			3: "😐",
			4: "😊",
			5: "🥳",
		} as MoodEmojis,
	};

	modalRef: any;

	getAverageMood(): number {
		if (!this.day.moods?.length) return 0;
		const sum = this.day.moods.reduce((acc, mood) => acc + mood.moodValue, 0);
		return Math.round(sum / this.day.moods.length);
	}

	getEmoji(): string {
		const avgMood = this.getAverageMood();
		return avgMood ? this.emojis.mood[avgMood as MoodValue] : this.emojis.noMood;
	}

	OpenModal() {
		this.modalRef = this.modalService.show(CalendarMoodInformationModalComponent);
		this.modalRef.content.moods = this.day.moods || [];
	}

	GetMoodCount() {
		return this.day.moods?.length || 0;
	}

	handleDayClick() {
		if (this.GetMoodCount() > 0) {
			this.OpenModal();
		} else {
			const selectedDate = new Date(new Date().getFullYear(), new Date().getMonth(), this.day.dayOfMonth);
			this.router.navigate(["/dashboard"], {
				queryParams: { date: selectedDate.toISOString() },
			});
		}
	}
}

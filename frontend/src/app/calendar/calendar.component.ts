import { Component, OnInit } from "@angular/core";
import { Day, Mood } from "../types";
import { MoodService } from "../services/mood.service";

@Component({
	selector: "app-calendar",
	templateUrl: "./calendar.component.html",
})
export class CalendarComponent implements OnInit {
	currentDate = new Date();
	selectedMonth = this.currentDate.getMonth();
	selectedYear = this.currentDate.getFullYear();
	days: Day[] = [];
	moods: Mood[] = [];

	constructor(private moodService: MoodService) {}

	ngOnInit() {
		this.initializeDays();
		this.loadMoods();
	}

	getMonthName(): string {
		return new Date(this.selectedYear, this.selectedMonth).toLocaleString("en-us", { month: "long" });
	}

	previousMonth() {
		if (this.selectedMonth === 0) {
			this.selectedMonth = 11;
			this.selectedYear--;
		} else {
			this.selectedMonth--;
		}
		this.initializeDays();
		this.mapMoodsToDays();
	}

	nextMonth() {
		const now = new Date();
		const nextMonth = this.selectedMonth === 11 ? 0 : this.selectedMonth + 1;
		const nextYear = this.selectedMonth === 11 ? this.selectedYear + 1 : this.selectedYear;

		if (nextYear > now.getFullYear() || (nextYear === now.getFullYear() && nextMonth > now.getMonth())) {
			return;
		}

		this.selectedMonth = nextMonth;
		this.selectedYear = nextYear;
		this.initializeDays();
		this.mapMoodsToDays();
	}

	private initializeDays() {
		const daysInMonth = new Date(this.selectedYear, this.selectedMonth + 1, 0).getDate();
		const isCurrentMonth =
			this.selectedMonth === this.currentDate.getMonth() && this.selectedYear === this.currentDate.getFullYear();
		const maxDay = isCurrentMonth ? this.currentDate.getDate() : daysInMonth;

		this.days = Array.from({ length: daysInMonth }, (_, i) => i + 1)
			.map((dayOfMonth): Day => {
				const dayName = new Date(this.selectedYear, this.selectedMonth, dayOfMonth).toLocaleString("en-us", {
					weekday: "long",
				});
				return { dayOfMonth, dayName, isLast: false };
			})
			.filter((day) => day.dayOfMonth <= maxDay);

		this.days[this.days.length - 1].isLast = true;
	}

	private loadMoods() {
		this.moodService.getMoods().subscribe({
			next: (response: any) => {
				this.moods = response;
				this.mapMoodsToDays();
			},
			error: (error) => {
				console.error("Error fetching moods:", error);
			},
		});
	}

	private mapMoodsToDays() {
		this.days = this.days.map((day) => {
			const dayDate = new Date(this.selectedYear, this.selectedMonth, day.dayOfMonth);

			const dayMoods = this.moods.filter((m) => {
				const moodDate = new Date(m.date);
				return (
					moodDate.getDate() === dayDate.getDate() &&
					moodDate.getMonth() === dayDate.getMonth() &&
					moodDate.getFullYear() === dayDate.getFullYear()
				);
			});

			return {
				...day,
				mood: dayMoods[0], // keep for backwards compatibility
				moods: dayMoods,
			};
		});
	}
}

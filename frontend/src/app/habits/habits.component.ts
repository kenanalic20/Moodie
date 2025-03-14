import { Component, OnInit } from "@angular/core";
import { HabitService } from "../services/habit.service";
import { Habit } from "../models/habit";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";

@Component({
	selector: "app-habits",
	templateUrl: "./habits.component.html",
})
export class HabitsComponent implements OnInit {
	habits: Habit[] = [];
	newHabitName = "";
	newHabitDesc = "";

	constructor(
		private habitService: HabitService,
		private toastr: ToastrService,
		private translateService: TranslateService,
	) {}

	ngOnInit() {
		this.loadHabits();
	}

	loadHabits() {
		this.habitService.getHabits().subscribe({
			next: (habits) => {
				this.habits = habits;
			},
			error: (error) => {
				this.toastr.error(
					this.translateService.instant("Failed to load habits"),
					this.translateService.instant("Error"),
				);
				console.error(error);
			},
		});
	}

	addHabit() {
		if (!this.newHabitName.trim()) {
			this.toastr.error(
				this.translateService.instant("Habit name is required"),
				this.translateService.instant("Error"),
			);
			return;
		}

		this.habitService.createHabit(this.newHabitName, this.newHabitDesc).subscribe({
			next: (response) => {
				this.loadHabits();
				this.newHabitName = "";
				this.newHabitDesc = "";

				if (response.newAchievements?.length > 0) {
					this.toastr.success(
						this.translateService.instant("You earned a new achievement!"),
						this.translateService.instant("Success"),
					);
				} else {
					this.toastr.success(
						this.translateService.instant("Habit added successfully"),
						this.translateService.instant("Success"),
					);
				}
			},
			error: (error) => {
				this.toastr.error(this.translateService.instant("Failed to add habit"), this.translateService.instant("Error"));
				console.error(error);
			},
		});
	}

	deleteHabit(id?: number) {
		if (!id) {
			this.toastr.error(this.translateService.instant("Invalid habit ID"), this.translateService.instant("Error"));
			return;
		}
		this.habitService.deleteHabit(id).subscribe({
			next: () => {
				this.loadHabits();
				this.toastr.success(
					this.translateService.instant("Habit deleted successfully"),
					this.translateService.instant("Success"),
				);
			},
			error: (error) => {
				this.toastr.error(
					this.translateService.instant("Failed to delete habit"),
					this.translateService.instant("Error"),
				);
				console.error(error);
			},
		});
	}

	checkIn(habit: Habit) {
		if (!habit.id) {
			this.toastr.error(this.translateService.instant("Invalid habit ID"), this.translateService.instant("Error"));
			return;
		}

		// Check if already checked in today
		if (!this.canCheckIn(habit)) {
			this.toastr.warning(
				this.translateService.instant("Already checked in today"),
				this.translateService.instant("Warning"),
			);
			return;
		}

		this.habitService.checkIn(habit.id).subscribe({
			next: (updatedHabit) => {
				const index = this.habits.findIndex((h) => h.id === habit.id);
				if (index !== -1) {
					this.habits[index] = updatedHabit;
				}
				this.toastr.success(
					this.translateService.instant("Check-in successful"),
					this.translateService.instant("Success"),
				);
			},
			error: (error) => {
				console.error(error);
				this.toastr.error(this.translateService.instant("Failed to check in"), this.translateService.instant("Error"));
			},
		});
	}

	canCheckIn(habit: Habit): boolean {
		return this.habitService.canCheckInToday(habit);
	}

	getTimeSinceLastCheckIn(lastCheckIn: string): string {
		if (!lastCheckIn) return this.translateService.instant("Never");

		const lastCheck = new Date(lastCheckIn);
		const now = new Date();
		const diffMs = now.getTime() - lastCheck.getTime();
		const diffDays = Math.floor(diffMs / (1000 * 60 * 60 * 24));

		if (diffDays === 0) return this.translateService.instant("Today");
		if (diffDays === 1) return this.translateService.instant("Yesterday");
		return `${diffDays} ${this.translateService.instant("days ago")}`;
	}
}

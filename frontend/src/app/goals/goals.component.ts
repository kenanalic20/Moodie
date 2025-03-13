import { Component, OnInit } from "@angular/core";
import { GoalService } from "../services/goal.service";
import { Goal } from "../models/goal";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";

@Component({
	selector: "app-goals",
	templateUrl: "./goals.component.html",
})
export class GoalsComponent implements OnInit {
	goals: Goal[] = [];

	constructor(
		private goalService: GoalService,
		private toastrService:ToastrService,
		private translateService:TranslateService
	) {}

	ngOnInit() {
		this.loadGoals();
	}

	loadGoals() {
		this.goalService.getGoals().subscribe((goals) => {
			this.goals = goals;
		});
	}

	deleteEntry(goalId: number) {
		this.goalService.deleteGoal(goalId).subscribe(() => {
			this.loadGoals();
		});
	}

	addGoal(nameInput: HTMLInputElement, descriptionInput: HTMLTextAreaElement, goalType: string) {
		if (!nameInput.value.trim()){
			this.toastrService.error(this.translateService.instant("Please insert title"),this.translateService.instant("Error"))
			return;
		}

		const newGoal: Goal = {
			name: nameInput.value,
			description: descriptionInput.value,
			goalType: goalType,
			startDate: new Date().toISOString(),
			endDate: new Date().toISOString(),
		};

		this.goalService.createGoal(newGoal).subscribe(() => {
			nameInput.value = "";
			descriptionInput.value = "";
			this.toastrService.success(this.translateService.instant("Goal added successfully"),this.translateService.instant("Success"))
			this.loadGoals();
		});
	}

	completeGoal(goal: Goal) {
		goal.completed = !goal.completed;
		this.goalService.updateGoal(goal).subscribe(() => {
			this.loadGoals();
		});
	}
}

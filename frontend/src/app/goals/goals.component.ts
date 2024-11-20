import { Component, OnInit } from "@angular/core";
import { GoalService } from "../services/goal.service";
import { Goal } from "../models/goal";

@Component({
	selector: "app-goals",
	templateUrl: "./goals.component.html",
})
export class GoalsComponent implements OnInit {
	goals: Goal[] = [];

	constructor(private goalService: GoalService) {}

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
		if (!nameInput.value.trim()) return;

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

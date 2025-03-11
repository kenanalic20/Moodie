import { Component, OnInit } from "@angular/core";
import { AchievementsService } from "../services/achievements.service";
import { UserAchievement } from "../models/achievement.model";
import { ToastrService } from "ngx-toastr";

@Component({
	selector: "app-achievements",
	templateUrl: "./achievements.component.html",
})
export class AchievementsComponent implements OnInit {
	userAchievements: UserAchievement[] = [];
	loading = false;
	error = false;

	constructor(
		private achievementsService: AchievementsService,
		private toastrService: ToastrService,
	) {}

	ngOnInit(): void {
		this.loading = true;
		this.achievementsService.getUserAchievements().subscribe({
			next: (achievements) => {
				this.userAchievements = achievements;
				this.loading = false;
			},
			error: (error) => {
				console.error("Error fetching achievements", error);
				this.toastrService.error("Failed to load achievements", "Error");
				this.loading = false;
				this.error = true;
			},
		});
	}
}

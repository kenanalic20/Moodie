import { Component, OnInit } from "@angular/core";
import { ChartConfiguration } from "chart.js";
import { MoodService } from "../../services/mood.service";

@Component({
	selector: "app-monthly-stats",
	templateUrl: "./monthly-stats.component.html",
})
export class MonthlyStatsComponent implements OnInit {
	public lineChartData: ChartConfiguration["data"] = {
		datasets: [
			{
				data: [],
				label: "Average Mood",
				backgroundColor: "rgba(148,159,177,0.2)",
				borderColor: "rgba(148,159,177,1)",
				pointBackgroundColor: "rgba(148,159,177,1)",
				fill: true,
			},
		],
		labels: [],
	};

	public lineChartOptions: ChartConfiguration["options"] = {
		responsive: true,
		scales: {
			y: {
				min: 1,
				max: 5,
			},
		},
	};

	constructor(private moodService: MoodService) {}

	ngOnInit() {
		this.moodService.getMoods().subscribe((moods: any[]) => {
			// Group moods by day and calculate averages
			const dailyAverages = this.calculateDailyAverages(moods);
			this.lineChartData.datasets[0].data = Object.values(dailyAverages);
			this.lineChartData.labels = Object.keys(dailyAverages);
		});
	}

	private calculateDailyAverages(moods: any[]): { [key: string]: number } {
		const dailyAverages: { [key: string]: number } = {};
		// Calculate averages for each day in the current month
		// Implementation details...
		return dailyAverages;
	}
}
